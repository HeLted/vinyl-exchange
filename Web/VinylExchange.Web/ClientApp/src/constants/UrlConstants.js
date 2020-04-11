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
  genreId: "genreId",
  searchTerm: "searchTerm",
  releasesToSkip: "releasesToSkip",
  shopsToSkip: "shopsToSkip",
  styleIds: "styleIds",
  returnUrl: "returnUrl",
  cofirmToken: "cofirmToken",
  filterGenreId: "filterGenreId"
};

export const Headers = { requestVerificationToken: "RequestVerificationToken" };

export const Controllers = {
  releases: {
    name: "/Releases",
    actions: { getReleases: "/GetReleases" }
  },
  genres: { name: "/Genres" },
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
    actions: {
      getAllSalesForRelease: "/GetAllSalesForRelease",
      placeOrder: "/PlaceOrder",
      setShippingPrice: "/SetShippingPrice",
      completePayment: "/CompletePayment",
      getUserSales: "/GetUserSales",
      getUserPurchases: "/GetUserPurchases",
      confirmItemSent: "/ConfirmItemSent",
      confirmItemRecieved: "/ConfirmItemRecieved",
      cancelOrder:"/CancelOrder"
    }
  },
  users: {
    name: "/Users",
    actions: {
      register: "/Register",
      login: "/Login",
      confirmEmail: "/ConfirmEmail",
      changeAvatar: "/ChangeAvatar",
      getCurrentUserAvatar: "/GetCurrentUserAvatar",
      getUserAvatar: "/GetUserAvatar",
      sendConfirmEmail: "/SendConfirmEmail",
      sendChangeEmailEmail: "/SendChangeEmailEmail",
      sendChangePasswordEmail: "/SendChangePasswordEmail",
      sendResetPasswordEmail: "/SendResetPasswordEmail",
      changeEmail: "/ChangeEmail",
      resetPassword: "/ResetPassword"
    }
  },
  addresses: {
    name: "/Addresses",
    actions: {
      getUserAddresses: "/GetUserAddresses"
    }
  }
};
