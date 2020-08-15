﻿using AlloyAdvanced.Models.Media;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAccess;
using EPiServer.Framework.Blobs;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Security;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Configuration;

namespace AlloyAdvanced.Business.ScheduledJobs
{
    [ScheduledPlugIn(DisplayName = ScheduledJobName,
        GUID = "88E83CA0-91ED-46FD-A338-7938C8D0FDF9", 
        SortIndex = -1, Restartable = true)]
    public class ImportImagesScheduledJob : ScheduledJobBase
    {
        public const string ScheduledJobName = "Import Images";

        private readonly string[] patterns = new[] { "*.png", "*.jpeg", "*.jpg" };

        private readonly IContentRepository contentRepository;
        private readonly IBlobFactory blobFactory;

        private bool _stopSignaled;

        public ImportImagesScheduledJob()
        {
            IsStoppable = true;
        }

        public ImportImagesScheduledJob(
            IContentRepository contentRepository,
            IBlobFactory blobFactory) : this()
        {
            this.contentRepository = contentRepository;
            this.blobFactory = blobFactory;
        }

        public override void Stop()
        {
            _stopSignaled = true;
        }

        private IEnumerable<string> GetImageFilenames(string path)
        {
            IEnumerable<string> files = null;
            foreach (string pattern in patterns)
            {
                IEnumerable<string> moreFiles = Directory.GetFiles(path, pattern);
                if (files == null) files = moreFiles;
                else files.Concat(moreFiles);
            }
            return files;
        }

        public override string Execute()
        {
            string toImportFolder = WebConfigurationManager.AppSettings["episerver:edu.ToImportFolder"];
            string importedFolder = WebConfigurationManager.AppSettings["episerver:edu.ImportedFolder"];

            var assetsFolder = new ContentReference(
                WebConfigurationManager.AppSettings["episerver:edu.ImportAssetsFolder"]);

            IEnumerable<string> images = GetImageFilenames(toImportFolder);
            int toImportCount = images.Count();
            int importedCount = 0;
            int remainingCount = toImportCount;

            OnStatusChanged($"Starting {ScheduledJobName}. {toImportCount} images to import. Please wait...");

            while (remainingCount > 0)
            {
                if (_stopSignaled)
                {
                    return "Stop of job was called";
                }

                string nextImage = images.First();

                var asset = contentRepository.GetDefault<ImageFile>(parentLink: assetsFolder);
                asset.Name = Path.GetFileName(nextImage);
                asset.Copyright = $"Copyright © 2018 Episerver Education";

                Blob blob = blobFactory.CreateBlob(id: asset.BinaryDataContainer, 
                    extension: Path.GetExtension(nextImage));
                blob.WriteAllBytes(File.ReadAllBytes(nextImage));
                asset.BinaryData = blob;

                contentRepository.Save(asset, SaveAction.Publish, AccessLevel.NoAccess);

                File.Move(nextImage, Path.Combine(
                    importedFolder, Path.GetFileName(nextImage)));

                Thread.Sleep(2500); // slow it down
                importedCount++;

                OnStatusChanged($"Imported {importedCount} of {toImportCount} images. Please wait...");

                images = GetImageFilenames(toImportFolder);
                remainingCount = images.Count();
            }

            return $"Successfully imported {importedCount} images.";
        }
    }
}