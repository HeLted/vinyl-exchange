import React from 'react';
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faCompactDisc} from "@fortawesome/free-solid-svg-icons";

function PageSpinner(){
    return(<div className="d-flex justify-content-center align-items-center" style={{height:"400px",with:"500"}}>
   
    <FontAwesomeIcon icon={faCompactDisc} size="3x"  spin/>
  </div>)
}

export default PageSpinner;