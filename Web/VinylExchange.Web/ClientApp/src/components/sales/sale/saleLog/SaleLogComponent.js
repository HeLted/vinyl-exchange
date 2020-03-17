import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";

function SaleLogComponent(props) {
  const listItems = props.data.logs.map(logObj => {
    return (
      <li key={logObj.id}>
        <div className="row text-left">
          <div className="col-lg-12 col-sm-12">
            <div className="alert alert-secondary" role="alert">
              {new Date(logObj.createdOn).toLocaleString() + " "}   <FontAwesomeIcon icon={faArrowRight} /> { " " + logObj.logContent }
            </div>
          </div>
          
        </div>
      </li>
    );
  });

  return (
    <div className="border sale-log-container">
      <ul>
        {" "}
        {props.data.isLoading ? (
          <BorderSpinner />
        ) : listItems.length > 0 ? (
          listItems
        ) : (
          <div><h6 className="property-text">Log is empty</h6></div>
        )}
      </ul>
    </div>
  );
}

export default SaleLogComponent;
