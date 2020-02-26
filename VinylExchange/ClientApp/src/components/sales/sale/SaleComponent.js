import React from "react";
import "./Sale.css";
import SaleStatusBar from "./saleStatus/SaleStatusBar"

function SaleComponent(props) {
  return (
    <div className="row">
      <div className="col-4">
        <SaleStatusBar data={{status:props.status}}/>
        <br />
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
        <input type="text" />
      </div>
    </div>
  );
}

export default SaleComponent;
