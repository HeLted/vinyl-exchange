import React, { Fragment } from "react";
import PlayerLoaderButton from "./../../../common/PlayerLoaderButton";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function SaleReleaseInfoComponent(props) {
  return (props.data.isLoading ?(<BorderSpinner/>) : (
    <Fragment>
      <div className="col-2 ">
        <img
          className="img-thumbnail"
          height="100px"
          width="100px"
          src={props.data.coverArt}
        ></img>
      </div>
      <br />
      <div className=" col-10  text-left">
        <div className="row">
          <div className="col-11">
            <h3 className="property-text-lm">
              {props.data.artist} - {props.data.title}{" "}
            </h3>
          </div>
          <div className="sale-button-container col-1  text-center">
            <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />
          </div>
        </div>
        <div className="row border border-dark" style={{backgroundColor:"black"}}></div>
      </div>
    </Fragment>)
  );
}

export default SaleReleaseInfoComponent;
