import React from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faExclamationTriangle } from "@fortawesome/free-solid-svg-icons";

function ClientError(props){
   return( <div>
        <div className="container py-5 border">
          <div className="row justify-content-center text-center">
          <div className="col-md-2 text-center">
              <p>
                <FontAwesomeIcon icon={faExclamationTriangle} size="5x"/>
                <br />
                Status Code: {props.statusCode}
              </p>
            </div>
           </div>
          <div className="row justify-content-center text-center" >
            
            <div className="col-md-10">
              <h3>{props.heading}</h3>
              <p>
                {props.info}
              </p>
              <a className="btn btn-danger" href="javascript:history.back()">
                Go Back
              </a>
            </div>
          </div>
        </div>
      </div>);
}

export default ClientError