export const Url = {
  api: "/api",
  authentication: "/authentication",
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
  genreId: "genreId",
  searchTerm: "searchTerm",
  releasesToSkip: "releasesToSkip",
  shopsToSkip: "shopsToSkip",
  styleIds: "styleIds",
  returnUrl: "returnUrl",
  cofirmToken: "cofirmToken"
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
    actions: { deleteAll: "/DeleteAll" }
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
    actions: {
      getUserCollection: "/GetUserCollection",
      doesUserCollectionContainRelease: "/DoesUserCollectionContainRelease"
    }
  },
  shops: {
    name: "/Shops",
    actions: { getShops: "/GetShops" }
  },
  sales: {
    name: "/Sales",
    actions: { getAllSalesForRelease: "/GetAllSalesForRelease" }
  },
  users: {
    name: "/Users",
    actions: { register: "/Register", login: "/Login" ,confirmEmail:"/ConfirmEmail"}
  }
};
