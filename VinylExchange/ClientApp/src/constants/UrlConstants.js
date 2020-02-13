export const Url = {
  api: "/api",
  mediaStorage: "/file/media",
  releaseIdQuery:"?releaseId="
};

export const Controllers = {
  releases: { name: "/Releases" },
  releaseImages: {
    name: "/ReleaseImages",
    actions: { getCoverArtForRelease: "/GetCoverArtForRelease" }
  },
  releaseTracks: {
    name: "/ReleaseTracks",
    actions: { getAllTracksForRelease: "/GetAllTracksForRelease" }
  }
};
