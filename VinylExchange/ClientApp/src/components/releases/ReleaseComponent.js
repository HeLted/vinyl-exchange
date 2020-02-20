import React from "react";
import "./ReleasePage.css"
import ReleaseImagesCarouselContainer from "./ReleaseImagesCarouselContainer";
import authService from "./../api-authorization/AuthorizeService";
import ReleaseMenuContainer from "./ReleaseMenuContainer"

function ReleaseComponent(props) {
  const user = authService.getUser();
  console.log(user);

  return (
    <div className="container-fluid">
      <div className="row">
        <div className="col-3">
          <ReleaseImagesCarouselContainer
            data={{ releaseId: props.data.releaseId }}
          />
        </div>
        <div className="col-8">
          <h1>Info Component</h1>
        </div>
        <div className="col-1 ">
        <ReleaseMenuContainer data={{ releaseId: props.data.releaseId}}/>
        </div>
      
      </div>
       
    </div>
  );
}

export default ReleaseComponent;
