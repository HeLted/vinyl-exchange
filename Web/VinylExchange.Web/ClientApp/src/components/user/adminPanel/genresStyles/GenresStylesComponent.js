import React, { Fragment } from "react";
import GenresContainer from "./genres/GenresContainer";
import StylesContainer from "./styles/StylesContainer";

function GenreStylesComponent(props) {
  return (
    <Fragment>
      <div className="col-12">
        <GenresContainer
          functions={{ handleReLoad: props.functions.handleReLoad }}
        />
      </div>
      <div className="col-12">
        <StylesContainer 
         functions={{ handleReLoad: props.functions.handleReLoad }}/>
      </div>
    </Fragment>
  );
}

export default GenreStylesComponent
