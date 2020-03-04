import React from "react";
import SearchReleasesContainer from "./searchReleases/SearchReleasesContainer";
import ReleasesContainerWrapper from "./releases/ReleasesContainer";
import FilterReleasesContainer from "./filterReleases/FilterReleasesContainer";
import { withRouter } from "react-router-dom";
import "./Marketplace.css";

function MarketplaceComponent(props) {
  

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="custom-container col-lg-3 col-sm-12 col-xs-12 center-block">
          <div className="container-fluid justify-content-center">
            <SearchReleasesContainer
              onUpdateSearchValue={props.functions.onUpdateSearchValue}
              searchValue={props.data.searchValue}
            />

            <FilterReleasesContainer
              functions={{
                onUpdateFilterValue: props.functions.onUpdateFilterValue
              }}
            />

            <br />
            
          </div>
        </div>
        <div className="custom-container col-lg-9 col-sm-12 col-xs-12 align-items-center justify-content-center">
          <ReleasesContainerWrapper
            data={{
              searchValue: props.data.searchValue,
              filterStyleIds: props.data.filterStyleIds
            }}
          />
        </div>
        
      </div>
     
    </div>
  );
}

export default MarketplaceComponent;
