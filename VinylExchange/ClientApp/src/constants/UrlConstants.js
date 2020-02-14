export const Url = {
  api: "/api",
  mediaStorage: "/file/media",
  releaseIdQuery: "?releaseId="
};

export const Controllers = {
  releases: {
    name: "/Releases",
    actions: { getReleases: "/GetReleases", create: "/Create" }
  },
  genres: { name: "/Genres", actions: { getAllGenres: "/GetAllGenres" } },
  styles: {
    name: "/Styles",
    actions: { getAllStylesForGenre: "/GetAllStylesForGenre" }
  },
  files: {
    name: "/Files",
    actions: { upload: "/Upload", delete: "/Delete", deleteAll: "/DeleteAll" }
  },
  releaseImages: {
    name: "/ReleaseImages",
    actions: { getCoverArtForRelease: "/GetCoverArtForRelease" }
  },
  releaseTracks: {
    name: "/ReleaseTracks",
    actions: { getAllTracksForRelease: "/GetAllTracksForRelease" }
  }
};
