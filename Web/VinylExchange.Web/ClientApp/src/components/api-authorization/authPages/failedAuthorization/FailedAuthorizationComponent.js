import React, { Component } from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faExclamationTriangle } from "@fortawesome/free-solid-svg-icons";


class FailedAuthorizationComponent extends Component {
 
  render() {
    return (
      <div>
        <div class="container py-5">
          <div class="row border" style={{padding:"50px"}}>
            <div class="col-md-2 text-center">
              <p>
                <FontAwesomeIcon icon={faExclamationTriangle} size="5x"/>
                <br />
                Status Code: 403
              </p>
            </div>
            <div class="col-md-10">
              <h3>OPPSSS!!!! Sorry...</h3>
              <p>
                Sorry, your access is refused due to security reasons of our
                server and also our sensitive data.
                <br />
                Please go back to the previous page to continue browsing.
              </p>
              <a class="btn btn-danger" href="javascript:history.back()">
                Go Back
              </a>
            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default FailedAuthorizationComponent;
