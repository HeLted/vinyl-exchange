import React from "react";
import SearchReleasesContainer from "./searchReleases/SearchReleasesContainer";
import ReleasesContainer from "./releases/ReleasesContainer";

export default function MarketplaceComponent() {
  return (
    <div className="container-fluid">
      <div className="row">
        <div className="custom-container col-3 center-block">
          <div className="container-fluid justify-content-center">
            <SearchReleasesContainer />
          </div>
        </div>
        <div className="custom-container col-9">
          <ReleasesContainer />
        </div>
      </div>
    </div>
  );
}
