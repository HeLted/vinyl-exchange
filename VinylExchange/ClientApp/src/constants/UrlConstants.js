export const Url = {
  api: "/api",
  mediaStorage: "/file/media",
  and:"&",
  equal:"=",
  queryStart:"?",
  slash:"/"
};

export const Queries={

  releaseId:"releaseId",
  userId:"userId",
  formSessionId:"formSessionId"
}

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
    actions: { getCoverArtForRelease: "/GetCoverArtForRelease" ,getAllImagesForRelease:"/GetAllImagesForRelease"}
  },
  releaseTracks: {
    name: "/ReleaseTracks",
    actions: { getAllTracksForRelease: "/GetAllTracksForRelease" }
  },
  collections:{
    name:"/Collections",
    actions:{add:"/Add"}
  }
};
