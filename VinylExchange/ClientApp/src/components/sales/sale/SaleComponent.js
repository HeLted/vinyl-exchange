import React from "react";
import "./Sale.css";
import SaleStatusBar from "./saleStatus/SaleStatusBar";
import SaleInfoContainer from "./saleInfo/SaleInfoContainer";
import SaleMenuContainer from "./saleMenu/SaleMenuContainer";
import StatusBadge from "./../../common/badges/StatusBadge"

function SaleComponent(props) {
  return (
    <div className="container-fluid justify-content-center">
      <div className="row">
        <div className="menu-container col-12 border text-center">
          <SaleMenuContainer
            data={{ sale: props.data.sale }}
            functions={{ handleReLoadSale: props.functions.handleReLoadSale }}
          />
        </div>
        <div className="sale-info-container col-6 border justify-content-center text-center">
          <SaleInfoContainer data={{ sale: props.data.sale }} />
        </div>
        <div className="sale-info-container col-6 border ">
          <div className="row text-center border border-dark justify-content-center">
            <h5 className="property-text-lm">Sale Status</h5>
          </div>
          <br />
          <div className="row text-center  justify-content-center">
            <h5 className="property-text-nm"> <StatusBadge data={{status:props.data.sale.status}}/></h5>
            <br />
          </div>

          <br/>

          <SaleStatusBar data={{ status: props.data.sale.status }} />

          <br />
          <div className="row">
            <div className="col-12">
              <div className="chat-container border">
                <ul>
                  <li>
                    <div className="alert alert-primary" role="alert">
                      pesho:hei
                    </div>
                  </li>
                  <li>
                    <div className="alert alert-primary" role="alert">
                      gosho:hei
                    </div>
                  </li>
                </ul>
                <input type="text" />
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default SaleComponent;
