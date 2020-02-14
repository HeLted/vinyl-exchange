import React,{Fragment,Component} from "react";
import GenreFilterComponent from "./GenreFilterComponent";
import StyleFilterComponent from "./StyleFilterComponent";

class FilterReleasesComponent extends Component {
  render() {
    return (
      <Fragment>
        <GenreFilterComponent />
        <StyleFilterComponent />
      </Fragment>
    );
  }
}

export default FilterReleasesComponent;