import React from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faUserCog} from "@fortawesome/free-solid-svg-icons";

function PageSpinner(){
    return(<div className="d-flex justify-content-center align-items-center" style={{height:"400px",with:"500"}}>
   
    <FontAwesomeIcon icon={faUserCog} size="4x" spin/>
  </div>)
}

export default PageSpinner;