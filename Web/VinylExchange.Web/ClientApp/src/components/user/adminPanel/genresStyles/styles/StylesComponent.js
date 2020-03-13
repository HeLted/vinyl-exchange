import React from "react";
import AddStylesContainer from "./addStyles/AddStylesContainer";
import RemoveStylesContainer from "./removeStyles/RemoveStylesContainer";

function StylesComponent(props) {
  return (
    <div className="row ">
      <div className="col-12 border">
        <h3 className="property-text">Add Styles</h3>
      </div>
      <div className="col-12 border p-30">
        <AddStylesContainer
          data={{ genres: props.data.genres }}
          functions={{ handleReLoad: props.functions.handleReLoad }}
        />
      </div>
      <div className="col-12 border">
        <h3 className="property-text">Remove Styles</h3>
      </div>
      <div className="col-12 border p-30">
        <RemoveStylesContainer
          data={{ genres: props.data.genres }}
          functions={{ handleReLoad: props.functions.handleReLoad }}
        />
      </div>
    </div>
  );
}

export default StylesComponent;
