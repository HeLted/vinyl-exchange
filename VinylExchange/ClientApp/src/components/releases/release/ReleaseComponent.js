import React from "react";
import "./ReleasePage.css";
import ReleaseImagesCarouselContainer from "./releaseImageCarousel/ReleaseImagesCarouselContainer";
import ReleaseMenuContainer from "./releaseMenu/ReleaseMenuContainer";
import ReleaseSalesContainer from "./releaseSales/ReleaseSalesContainer";
import ReleaseInfoContainer from "./releaseInfo/ReleaseInfoContainer";
import ReleaseTracklistContainer from "./releaseTracklist/ReleaseTracklistContainer"

function ReleaseComponent(props) {
  return (
    <div className="container-fluid">
      <div className="row">
        <div className=" col-3">
          <ReleaseImagesCarouselContainer
            data={{ releaseId: props.data.releaseId }}
          />
        </div>

        
        <div className="col-lg-9 col-sm-12">
          <div className=" transparent-border release-info-container border row">
            <div className="col-3">
              <ReleaseInfoContainer
                data={{ releaseId: props.data.releaseId }}
              />
            </div>
          </div>

          <div className="transparent-border button-menu-container border row ">
            <div className="col-9">
              <ReleaseMenuContainer
                data={{ releaseId: props.data.releaseId }}
              />
            </div>
          </div>
        </div>
        <br/>
        <div className="release-tracklist-container border col-lg-4 col-sm-12">
          <div className=" row justify-content-center">
            <h2>Tracklist</h2>
          </div>
          <br />
          <div className="row justify-content-center">
            <div className="col-12">
             <ReleaseTracklistContainer data={{ releaseId: props.data.releaseId }}/>
            </div>
          </div>
        </div>
        <div className="release-sales-container border col-lg-8 col-sm-12">
          <div className=" row justify-content-center">
            <h2>Sales</h2>
          </div>
          <br />
          <div className="row justify-content-center">
            <div className="col-12">
            <ReleaseSalesContainer  data={{ releaseId: props.data.releaseId }} />
            </div>
          </div>
        </div>
      </div>
      <br />
      
    </div>
  );
}

export default ReleaseComponent;
