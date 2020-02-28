import React from "react";
import "./Sale.css";
import SaleStatusBar from "./saleStatus/SaleStatusBar";
import SaleInfoContainer from "./saleInfo/SaleInfoContainer";

function SaleComponent(props) {
  return (
    <div className="container-fluid justify-content-center">
      <div className="row">
        <div className="col-3 border text-center">
          <h6 className="property-text">Sale Status: {props.data.sale.statusText}</h6>
          <SaleStatusBar data={{ status: props.data.sale.status }} />
        </div>
        <div className="sale-info-container col-6 border">
          <SaleInfoContainer data={{ sale: props.data.sale }} />
        </div>
        <div className="menu-container col-3 border text-center">
          <button className="btn btn-success btn-lg">Place Order</button>
        </div>
      </div>
      <div className="row">
        <div className="col-6">
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
          <input type="text" />
        </div>
        </div>
      </div>
    </div>
  );
}

export default SaleComponent;
