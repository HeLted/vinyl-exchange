import React from "react";
import SearchReleasesContainer from "./searchReleases/SearchReleasesContainer";
import ReleasesContainerWrapper from "./releases/ReleasesContainer";
import FilterReleasesContainer from "./filterReleases/FilterReleasesContainer"
import { withRouter } from "react-router-dom";

export default function MarketplaceComponent(props) {
  const Button = withRouter(({ history }) => (
    <button
      type="button"
      className="btn btn-primary w-100"
      onClick={() => {
        history.push("/releases/addrelease");
      }}
    >
      Add Release
    </button>
  ));

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="custom-container col-lg-3 col-sm-12 col-xs-12 center-block">
          <div className="container-fluid justify-content-center">
            <SearchReleasesContainer
              onUpdateSearchValue={props.onUpdateSearchValue}
              searchValue={props.searchValue}
            />
            <FilterReleasesContainer/>
            <br />
            <Button />
          </div>
        </div>
        <div className="custom-container col-lg-9 col-sm-12 col-xs-12 align-items-center justify-content-center">
          <ReleasesContainerWrapper searchValue={props.searchValue} />
         
        </div>
      </div>
    </div>
  );
}
