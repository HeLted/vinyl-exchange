import React from "react";
import "./Sale.css";
import SaleStatusBar from "./saleStatus/SaleStatusBar";
import SaleInfoContainer from "./saleInfo/SaleInfoContainer";
import SaleMenuContainer from "./saleMenu/SaleMenuContainer";
import StatusBadge from "./../../common/badges/StatusBadge";
import SaleChatContainer from "./saleChat/SaleChatContainer";
import PageSpinner from "./../../common/spinners/PageSpinner";

function SaleComponent(props) {
  return props.data.isLoading ? (
    <PageSpinner />
  ) : (
    <div className="container-fluid justify-content-center">
      <div className="row">
        <div className="sale-info-container col-6 border justify-content-center text-center">
          <SaleInfoContainer data={{ sale: props.data.sale }} />
        </div>
        <div className="sale-info-container col-6 border  ">
          <div className="row text-center border border-dark justify-content-center">
            <h5 className="property-text-lm">Sale Status</h5>
          </div>
          <br />
          <div className="row text-center  justify-content-center">
            <h5 className="property-text-nm">
              {" "}
              <StatusBadge data={{ status: props.data.sale.status }} />
            </h5>
            <br />
          </div>

          <br />

          <SaleStatusBar data={{ status: props.data.sale.status }} />

          <br />
          <div className="row justify-content-center align-self-center">
            <div className="col-12 ">
              {props.data.sale.status !== 1 ? (
                <SaleChatContainer data={{ sale: props.data.sale }} />
              ) : (
                <h1>Place order to initiate chat</h1>
              )}
            </div>
          </div>
        </div>
        <div className="menu-container col-6 border text-center">
          <h1>Sale Log Container</h1>
        </div>
        <div className="menu-container col-6 border text-center">
          <SaleMenuContainer
            data={{ sale: props.data.sale }}
            functions={{ handleReLoadSale: props.functions.handleReLoadSale }}
          />
        </div>
      </div>
    </div>
  );
}

export default SaleComponent;
