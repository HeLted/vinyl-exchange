import React from "react";
import "./Sale.css"

function SaleComponent() {
  return (
    <div className="row">
      <div className="col-4">
        <div class="progress">
          <div
            class="progress-bar progress-bar-striped progress-bar-animated"
            role="progressbar"
            aria-valuenow="75"
            aria-valuemin="0"
            aria-valuemax="100"
            style={{ width: "75%" }}
          ></div>
        </div>
        <br/>
        <div className="chat-container border">
          <ul>
            <li>
              <div class="alert alert-primary" role="alert">
                pesho:hei
              </div>
            </li>
            <li>
              <div class="alert alert-primary" role="alert">
                gosho:hei
              </div>
            </li>
          </ul>
         
        </div>
        <input type="text"/>
      </div>
    </div>
  );
}

export default SaleComponent;
