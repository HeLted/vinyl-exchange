import React, { Fragment, Component } from "react";
import GenreFilterComponent from "./GenreFilterComponent";
import StyleFilterComponent from "./StyleFilterComponent";

class FilterReleasesComponent extends Component {
  render() {
    return (
      <Fragment>
        <GenreFilterComponent
          data={{
            genres: this.props.data.genres,
            genreSelectInput: this.props.data.genreSelectInput
          }}
          functions={{ handleOnChange: this.props.functions.handleOnChange }}
        />
        <StyleFilterComponent
          data={{
            styles: this.props.data.styles,
            styleMultiSelectInput: this.props.data.styleMultiSelectInput
          }}
          functions={{
            handleOnChangeMultiSelect: this.props.functions.handleOnChangeMultiSelect
          }}
        />
      </Fragment>
    );
  }
}

export default FilterReleasesComponent;
