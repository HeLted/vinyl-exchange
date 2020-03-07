import React from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import PlaceOrderModalContainer from "./saleMenuModals/placeOrder/PlaceOrderModalContainer";
import SetShippingPriceModalContainer from "./saleMenuModals/setShippingPrice/SetShippingPriceModalContainer";
import PayNowModalContainer from "./saleMenuModals/payNow/PayNowModalContainer";
import InfoTooltip from "./../../../common/tooltips/InfoTooltip";

function SaleMenuComponent(props) {
  let component = null;
  const sale = props.data.sale;
  const currentUserId = props.data.currentUserId;

  if (sale.status === 1 && currentUserId === sale.sellerId) {
    component = (
      <div>
        <InfoTooltip
          data={{ tooltipValue: "Waiting For potential buyer..." }}
        />
        <button className="btn btn-danger btn-lg">Remove Sale</button>
        <button className="btn btn-primary btn-lg">Edit Sale</button>
      </div>
    );
  } else if (
    (sale.status === 1 && currentUserId === sale.buyerId) ||
    sale.buyerId == null
  ) {
    component = (
      <div>
        <InfoTooltip
          data={{
            tooltipValue:
              "You can place order if you are interested in this sale."
          }}
        />
        <button
          className="btn btn-success btn-lg"
          data-toggle="modal"
          data-target="#placeOrderModal"
        >
          Place Order
        </button>
        <PlaceOrderModalContainer
          data={{ saleId: sale.id }}
          functions={{
            handleReLoadSale: props.functions.handleReLoadSale
          }}
        />
      </div>
    );
  } else if (sale.status === 2 && currentUserId === sale.sellerId) {
    component = (
      <div>
        <InfoTooltip data={{ tooltipValue: "Please specify shipping price." }} />
        <button
          className="btn btn-success btn-lg"
          data-toggle="modal"
          data-target="#setShippingPriceModal"
        >
          Set Shipping Price
        </button>
        <SetShippingPriceModalContainer
          data={{ saleId: sale.id }}
          functions={{
            handleReLoadSale: props.functions.handleReLoadSale
          }}
        />
      </div>
    );
  } else if (sale.status === 2 && currentUserId === sale.buyerId) {
    component = (
      <div>
        <InfoTooltip data={{ tooltipValue: "Awaiting saller to specify shipping price..." }} />
      </div>
    );
  } else if (sale.status === 3 && currentUserId === sale.buyerId) {
    component = (
      <div>
        <InfoTooltip data={{ tooltipValue: "You can now pay for your order." }} />
        <button
          className="btn btn-success btn-lg"
          data-toggle="modal"
          data-target="#payNowModal"
        >
          Pay Now
        </button>
        <PayNowModalContainer
          data={{
            saleId: sale.id,
            price: sale.price,
            shippingPrice: sale.shippingPrice
          }}
          functions={{
            handleReLoadSale: props.functions.handleReLoadSale
          }}
        />
      </div>
    );
  } else if (sale.status === 3 && currentUserId === sale.sellerId) {
    component = (
      <div>
        <InfoTooltip data={{ tooltipValue: "Awaiting buyer to complete payment..." }} />
      </div>
    );
  }

  return props.data.isLoading ? <BorderSpinner /> : component;
}

export default SaleMenuComponent;
