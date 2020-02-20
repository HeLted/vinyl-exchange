import React, { Fragment, Component } from "react";
import GenreFilterComponent from "./GenreFilterComponent";
import StyleFilterComponent from "./StyleFilterComponent";

class FilterReleasesComponent extends Component {
  render() {
    return (
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
              <Fragment>
                <GenreFilterComponent
                  data={{
                    genres: this.props.data.genres,
                    genreSelectInput: this.props.data.genreSelectInput
                  }}
                  functions={{
                    handleOnChange: this.props.functions.handleOnChange
                  }}
                />
                <StyleFilterComponent
                  data={{
                    styles: this.props.data.styles,
                    styleMultiSelectInput: this.props.data.styleMultiSelectInput
                  }}
                  functions={{
                    handleOnChangeMultiSelect: this.props.functions
                      .handleOnChangeMultiSelect
                  }}
                />
              </Fragment>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default FilterReleasesComponent;
