namespace VinylExchange.Data.Seeding
{
    #region

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using VinylExchange.Common.Enumerations;
    using VinylExchange.Data.Models;
    using VinylExchange.Data.Seeding.Contracts;

    #endregion

    public class ReleaseFilesSeeder : ISeeder
    {
        public const string ImagesPath = @"\Image";

        public const string AudioFilesPath = @"\Audio";

        public async Task SeedAsync(VinylExchangeDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.ReleaseFiles.Any())
            {
                // Aphex Twin - Selected Ambient Works 85 - 92
                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"),
                    FileType.Image,
                    "e10ccdba-7f17-4831-a9e9-335a3f2ec96c@---@ZG93bmxvYWQ=.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"),
                    FileType.Audio,
                    "0d8f30d3-67a7-48b6-a2ad-f69d0afebed8@---@QXBoZXggVHdpbiAtIEFnZWlzcG9saXM=.mp3",
                    false);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("3cc8ee5c-4dc3-4009-b3fc-d768e6a564f8"),
                    FileType.Audio,
                    "863aad8d-a80a-453d-b9c8-010e4d84c564@---@QXBoZXggVHdpbiAtIFh0YWw=.mp3",
                    false);

                // De Lacy - Hideaway
                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("1cf99ed0-a565-4d2a-928b-99a0fd851d9b"),
                    FileType.Image,
                    "0002affb-a428-4279-bfc0-5202a1558850@---@Ui00MzgwMS0xMjM5MDM4Nzg2LmpwZWc=.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("1cf99ed0-a565-4d2a-928b-99a0fd851d9b"),
                    FileType.Audio,
                    "de61302f-01a7-446f-9d15-651ab4784d82@---@RGUgJ0xhY3kgSGlkZWF3YXkgW0RlZXAgRGlzaCBNaXhd.mp3",
                    false);

                // Sasha - Wavy Gravy 

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("22d663f8-6bd6-4f20-9f74-bd82da066e42"),
                    FileType.Image,
                    "210d3e68-f74d-45f1-9488-8890b58503fd@---@Ui0yMDg3Nzc1LTEzMzc3NjY3NzktNjg3OS5qcGVn.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("22d663f8-6bd6-4f20-9f74-bd82da066e42"),
                    FileType.Audio,
                    "129606d8-f943-4fbb-a21b-928af0252490@---@U2FzaGEgLSBXYXZ5IEdyYXZ5.mp3",
                    false);

                // Aphex Twin - Windowlicker                

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"),
                    FileType.Image,
                    "bf741066-f760-4a70-a213-8dcf7683d1d9@---@YTNkYTRiYmIzY2JkMTI0Mjk1NmFlNjAxMTVlMTA1MTA=.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"),
                    FileType.Image,
                    "3853fa95-e3d0-4ff3-ae01-3f7b8cb19d79@---@Ui0zNjY2LTE1Mzk5OTQ0OTgtNjI0OC5qcGVn.jpg",
                    false);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"),
                    FileType.Audio,
                    "1142773b-44ba-410d-a56d-857783594434@---@QXBoZXggVHdpbiAtIFdpbmRvd2xpY2tlcg==.mp3",
                    false);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("eb9101dc-d7d4-4558-8211-cdd9fd9d60f9"),
                    FileType.Audio,
                    "89b0c1f5-c8b4-4929-b5a5-b065ff0883d8@---@QXBoZXggVHdpbiAtIE5hbm5vdQ==",
                    false);

                // Squarepusher - Ultravisitor

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("4b3b4142-3621-4256-a209-4468a6c5ca4c"),
                    FileType.Image,
                    "4e192198-d5d0-4b3e-86dd-287814e2ee4a@---@dWx0cmF2aXNpdG9yLS0tLUNvdmVyQXJ0.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("4b3b4142-3621-4256-a209-4468a6c5ca4c"),
                    FileType.Audio,
                    "3a7f690f-4a0e-4d54-9f13-d9857e1a5d1d@---@U3F1YXJlcHVzaGVyIC0gSWFtYmljIDkgUG9ldHJ5.mp3",
                    false);

                // Mobb Deep - Shook Ones

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("6eaabd12-fd4f-4b69-b0bd-f172f9b42085"),
                    FileType.Image,
                    "b37bfc94-df37-4d32-9bbd-0d6b36ce50c8@---@Ui02ODQwNjgtMTQ5MzY0OTc5Ny0zODc2LmpwZWc=.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("6eaabd12-fd4f-4b69-b0bd-f172f9b42085"),
                    FileType.Image,
                    "86de218d-a345-4a74-a56a-835d5e435888@---@Ui02ODQwNjgtMTQ5MzY0OTc5OS05NzUzLmpwZWc=.jpg",
                    true);

                await SeedReleaseFileAsync(
                    dbContext,
                    Guid.Parse("6eaabd12-fd4f-4b69-b0bd-f172f9b42085"),
                    FileType.Audio,
                    "d682cea0-1bf6-4b3e-87e6-ced481e239bc@---@TW9iYiBEZWVwIC0gU2hvb2sgT25lcywgUHQuIElJ.mp3",
                    false);
            }
        }

        private static async Task SeedReleaseFileAsync(
            VinylExchangeDbContext dbContext,
            Guid releaseId,
            FileType fileType,
            string fileName,
            bool isPrewiew)
        {
            var releaseFile = new ReleaseFile
                {
                    ReleaseId = releaseId,
                    Path = @"\Releases\" + fileType + "\\",
                    FileName = fileName,
                    IsPreview = isPrewiew,
                    FileType = fileType
                };

            await dbContext.ReleaseFiles.AddAsync(releaseFile);

            await dbContext.SaveChangesAsync();
        }
    }
}