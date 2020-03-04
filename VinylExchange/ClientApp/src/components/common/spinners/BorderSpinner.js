import React from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCircleNotch} from "@fortawesome/free-solid-svg-icons";

function BorderSpinner(){
    return(<div className="d-flex justify-content-center">
    {/* <div className="spinner-border text-primary" role="status">
      <span className="sr-only">Loading...</span>
    </div> */}
    <FontAwesomeIcon icon={faCircleNotch} size="3x" color="#34e5eb" spin/>
  </div>)
}

export default BorderSpinner;