import React from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faExclamationTriangle } from "@fortawesome/free-solid-svg-icons";

function ClientError(props){
   return( <div>
        <div class="container py-5">
          <div class="row border" style={{padding:"50px"}}>
            <div class="col-md-2 text-center">
              <p>
                <FontAwesomeIcon icon={faExclamationTriangle} size="5x"/>
                <br />
                Status Code: {props.statusCode}
              </p>
            </div>
            <div class="col-md-10">
              <h3>{props.heading}</h3>
              <p>
                {props.info}
              </p>
              <a class="btn btn-danger" href="javascript:history.back()">
                Go Back
              </a>
            </div>
          </div>
        </div>
      </div>);
}

export default ClientError