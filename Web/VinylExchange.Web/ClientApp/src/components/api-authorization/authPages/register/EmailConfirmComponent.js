import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faSync } from "@fortawesome/free-solid-svg-icons";

function EmailConfirmContainer(props) {
  return (
    <div className="container-fluid d-flex justify-content-center">
      <div className="col-5 d-flex justify-content-center">
        {props.data.isLoading ? (
          <button className="btn btn-primary">
            <FontAwesomeIcon icon={faSync} spin />
          </button>
        ) : (
          <button
            className="btn btn-primary btn-lg btn-block"
            onClick={props.functions.handleOnClick}
          >
            Confirm Email
          </button>
        )}
      </div>
    </div>
  );
}

export default EmailConfirmContainer;
