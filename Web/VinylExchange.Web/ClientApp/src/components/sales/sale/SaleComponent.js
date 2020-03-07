import React from "react";
import "./Sale.css";
import SaleStatusBar from "./saleStatus/SaleStatusBar";
import SaleInfoContainer from "./saleInfo/SaleInfoContainer";
import SaleMenuContainer from "./saleMenu/SaleMenuContainer";
import StatusBadge from "./../../common/badges/StatusBadge";
import SaleChatContainer from "./saleChat/SaleChatContainer";
import PageSpinner from "./../../common/spinners/PageSpinner";
import SaleLogContainer from "./saleLog/SaleLogContainer";
import SaleReleaseInfoContainer from "./saleReleaseInfo/SaleReleaseInfoContainer";
import PlayerLoaderButton from "./../../common/PlayerLoaderButton";

function SaleComponent(props) {
  return props.data.isLoading ? (
    <PageSpinner />
  ) : (
    <div className="container-fluid justify-content-center">
      <div className="sale-release-info border row justify-content-left text-left">
        <SaleReleaseInfoContainer
          data={{ releaseId: props.data.sale.releaseId }}
        />
      </div>
      <div className="row">
        <div className="sale-info-container col-lg-6 col-md-12 col-sm-12 border justify-content-center text-center">
          <SaleInfoContainer data={{ sale: props.data.sale }} />
        </div>
        <div className="sale-info-container col-lg-6 col-md-12 col-sm-12 border  ">
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

          <br />

          <SaleStatusBar data={{ status: props.data.sale.status }} />

          <br />
          <div
            className="row border border-dark"
            style={{ backgroundColor: "black" }}
          ></div>
          <div className="row justify-content-center vertical-center text-center">
            <div className="chat-container align-content-center col-12">
              {props.data.isChatShown ? (
                props.data.sale.buyerId !== null ? (
                  <SaleChatContainer data={{ sale: props.data.sale }} />
                ) : (
                  <div className="search-not-found">
                    <h6>
                      <b>
                        <i>Chat is not active when sale is in open state!</i>
                      </b>
                    </h6>
                  </div>
                )
              ) : (
                <button
                  class="btn btn-outline-primary btn-lg"
                  onClick={props.functions.handleToggleChat}
                >
                  {" "}
                  Show Chat
                </button>
              )}
            </div>
               

            <div className="menu-container col-12 border text-center">
                  

              <SaleMenuContainer
                data={{ sale: props.data.sale }}
                functions={{
                  handleReLoadSale: props.functions.handleReLoadSale
                }}
              />
            </div>
          </div>
        </div>
        <div className="menu-container col-12 border text-center">
          <SaleLogContainer />
        </div>
      </div>
    </div>
  );
}

export default SaleComponent;
