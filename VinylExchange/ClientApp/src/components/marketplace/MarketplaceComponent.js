import React from "react";
import SearchReleasesContainer from "./searchReleases/SearchReleasesContainer";
import ReleasesContainerWrapper from "./releases/ReleasesContainer";
import FilterReleasesContainer from "./filterReleases/FilterReleasesContainer";
import { withRouter } from "react-router-dom";
import "./Marketplace.css";

function MarketplaceComponent(props) {
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
              onUpdateSearchValue={props.functions.onUpdateSearchValue}
              searchValue={props.data.searchValue}
            />
            <div className="accordion filter-collapse" id="filterCollapse">
              <div className="card">
                <div className="card-header" id="headingOne">
                  <h2 className="mb-0">
                    <button
                      className="filter-collapse-btn btn btn-outline-primary"
                      type="button"
                      data-toggle="collapse"
                      data-target="#collapseOne"
                      aria-expanded="true"
                      aria-controls="collapseOne"
                    >
                      Filter
                    </button>
                  </h2>
                </div>

                <div
                  id="collapseOne"
                  className="collapse show"
                  aria-labelledby="headingOne"
                  data-parent="#filterCollapse"
                >
                  <div className="filter-collapse-body card-body">
                    <FilterReleasesContainer
                      functions={{
                        onUpdateFilterValue: props.functions.onUpdateFilterValue
                      }}
                    />
                  </div>
                </div>
              </div>
            </div>

            <br />
            <Button />
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
