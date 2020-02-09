import React,{Fragment} from "react";
import GenreFilterComponent from "./GenreFilterComponent";
import StyleFilterComponent from "./StyleFilterComponent";

export default class FilterReleasesComponent extends React.Component {
  render() {
    return (
      <Fragment>
        <GenreFilterComponent />
        <StyleFilterComponent />
      </Fragment>
    );
  }
}
