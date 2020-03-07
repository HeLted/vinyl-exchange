import React from "react";
import { FontAwesomeIcon } from "@fortawesome/react-fontawesome";
import { faArrowRight } from "@fortawesome/free-solid-svg-icons";

function SaleLogComponent() {
  return (
    <div className="border sale-log-container">
      <ul>
        {" "}
        <li>
          <div className="row">
            <div className="col-2">
              <div class="alert alert-secondary" role="alert">
                05:27:48 
              </div>
            </div>
            <div className="col-1">
                <div class="alert alert-outline-primary" role="alert">
                <FontAwesomeIcon icon={faArrowRight} />
                </div>
              </div>
            <div className="col-9">
              <div class="alert alert-secondary" role="alert">
                This is a secondary alert—check it out!
              </div>
            </div>
          </div>
        </li>
        <li>
          <div className="row">
            <div className="col-2">
              <div class="alert alert-secondary" role="alert">
                05:27:48 
              </div>
            </div>
            <div className="col-1">
                <div class="alert alert-outline-primary" role="alert">
                <FontAwesomeIcon icon={faArrowRight} />
                </div>
              </div>
            <div className="col-9">
              <div class="alert alert-secondary" role="alert">
                This is a secondary alert—check it out!
              </div>
            </div>
          </div>
        </li>
      </ul>
    </div>
  );
}

export default SaleLogComponent;
