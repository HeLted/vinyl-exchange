import React from "react";
import SearchReleasesContainer from "./searchReleases/SearchReleasesContainer";
import ReleasesContainer from "./releases/ReleasesContainer";
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
        <div className="custom-container col-3 center-block">
          <div className="container-fluid justify-content-center">
            <SearchReleasesContainer
              onUpdateSearchValue={props.onUpdateSearchValue}
              searchValue={props.searchValue}
            />
            <br />
            <Button />
          </div>
        </div>
        <div className="custom-container col-9 align-items-center justify-content-center">
          <ReleasesContainer searchValue={props.searchValue} />
         
        </div>
      </div>
    </div>
  );
}
