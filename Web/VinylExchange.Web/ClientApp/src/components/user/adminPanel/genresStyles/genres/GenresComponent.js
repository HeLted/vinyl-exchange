import React from "react";
import AddGenresContainer from "./addGenres/AddGenresContainer";
import RemoveGenresContainer from "./removeGenres/RemoveGenresContainer";

function GenresComponent(props) {
  return (
    <div className="row ">
      <div className="col-12 border">
        <h3 className="property-text">Add Genres</h3>
      </div>
      <div className="col-12 border p-30">
        <AddGenresContainer
          functions={{
            handleReLoad: props.functions.handleReLoad
          }}
        />
      </div>
      <div className="col-12 border">
        <h3 className="property-text">Remove Genres</h3>
      </div>
      <div className="col-12 border p-30">
        <RemoveGenresContainer
          data={{ genres: props.data.genres }}
          functions={{
           
            handleReLoad: props.functions.handleReLoad
          }}
        />
      </div>
    </div>
  );
}

export default GenresComponent;
