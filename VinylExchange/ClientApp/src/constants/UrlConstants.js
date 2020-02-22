export const Url = {
  api: "/api",
  mediaStorage: "/file/media",
  and: "&",
  equal: "=",
  queryStart: "?",
  slash: "/"
};

export const Queries = {
  releaseId: "releaseId",
  userId: "userId",
  formSessionId: "formSessionId",
  genreId:"genreId",
  searchTerm:"searchTerm",
  releasesToSkip:"releasesToSkip",
  styleIds:"styleIds"
};

export const Controllers = {
  releases: {
    name: "/Releases",
    actions: { getReleases: "/GetReleases" }
  },
  genres: { name: "/Genres", actions: { getAllGenres: "/GetAllGenres" } },
  styles: {
    name: "/Styles",
    actions: { getAllStylesForGenre: "/GetAllStylesForGenre" }
  },
  files: {
    name: "/Files",
    actions: {  deleteAll: "/DeleteAll" }
  },
  releaseImages: {
    name: "/ReleaseImages",
    actions: {
      getCoverArtForRelease: "/GetCoverArtForRelease",
      getAllImagesForRelease: "/GetAllImagesForRelease"
    }
  },
  releaseTracks: {
    name: "/ReleaseTracks",
    actions: { getAllTracksForRelease: "/GetAllTracksForRelease" }
  },
  collections: {
    name: "/Collections",
    actions: { getUserCollection: "/GetUserCollection" , doesUserCollectionContainRelease:"/DoesUserCollectionContainRelease" }
  },
  shops:{
    name:"/Shops"
  }
};
