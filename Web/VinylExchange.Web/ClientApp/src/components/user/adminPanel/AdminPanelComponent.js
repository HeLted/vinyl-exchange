import React from "react";
import GenresStylesContainer from "./genresStyles/GenresStylesContainer";
import "./adminPanel.css"

function AdminPanelComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row border text-center">
        <div className="col-12 border-bottom">
          <h1 className="property-text">Administration Panel</h1>
        </div>
        <div className="col-6" style={{ padding: "50px" }}>
          <div className="row">
            <div className="col-12 border">
              <h1 className="property-text">Genres - Styles</h1>
            </div>
           
               <GenresStylesContainer/>


          </div>
        </div>
      </div>
    </div>
  );
}

export default AdminPanelComponent;
