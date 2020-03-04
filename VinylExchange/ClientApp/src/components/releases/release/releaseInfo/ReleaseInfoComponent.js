import React, { Component } from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function ReleaseInfoComponent(props) {
  const component = props.data.isLoading ? (
    <BorderSpinner />
  ) : (
    <ul>
      <li>
        <div className="border border-dark">
          <h5 className="property-text-lm">
            <b>
              Artist
            </b>
          </h5>
        </div>
        <h5 className="property-text-lm">
          <b>
            <i>{props.data.release.artist}</i>
          </b>
        </h5>
      </li>
      <li>
      <div className="border border-dark">
          <h5 className="property-text-lm">
            <b>
              Title
            </b>
          </h5>
        </div>
        <h5 className="property-text-lm">
          <b>
             <i>{props.data.release.title}</i>
          </b>
        </h5>
      </li>
      <li>
      <div className="border border-dark">
          <h5 className="property-text-lm">
            <b>
              Label
            </b>
          </h5>
        </div>
        <h5 className="property-text-lm">
          <b>
           <i>{props.data.release.label}</i>
          </b>
        </h5>
      </li>
      <li>
      <div className="border border-dark">
          <h5 className="property-text-lm">
            <b>
              Year
            </b>
          </h5>
        </div>
        <h5 className="property-text-lm">
          <b>
           <i>{props.data.release.year}</i>
          </b>
        </h5>
      </li>
      <li>
      <div className="border border-dark">
          <h5 className="property-text-lm">
            <b>
              Format
            </b>
          </h5>
        </div>
        <h5 className="property-text-lm">
          <b>
            <i>{props.data.release.format}</i>
          </b>
        </h5>
      </li>
    </ul>
  );

  return component;
}

export default ReleaseInfoComponent;
