import React from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import PlaceOrderModalContainer from "./saleMenuModals/placeOrder/PlaceOrderModalContainer";
import SetShippingPriceModalContainer from "./saleMenuModals/setShippingPrice/SetShippingPriceModalContainer";
import PayNowModalContainer from "./saleMenuModals/payNow/PayNowModalContainer";
import NoActionAvailableTextBox from "./../../../common/divTextBoxes/NoActionAvailableTextBox";
import ConfirmItemSentContainer from "./saleMenuModals/confirmItemSent/ConfirmItemSentContainer";
import ConfirmItemRecievedContainer from "./saleMenuModals/confirmItemRecieved/ConfirmItemRecievedContainer";

function SaleMenuComponent(props) {
  let component = null;
  const sale = props.data.sale;
  const currentUserId = props.data.currentUserId;

  if (sale.status === 1 && currentUserId === sale.sellerId) {
    component = (
      <div>
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
        <button
          className="btn btn-success btn-lg"
          data-toggle="modal"
          data-target="#setShippingPriceModal"
        >
          Set Shipping Price
        </button>
        <SetShippingPriceModalContainer data={{ saleId: sale.id }} />
      </div>
    );
  } else if (sale.status === 2 && currentUserId === sale.buyerId) {
    component = <NoActionAvailableTextBox />;
  } else if (sale.status === 3 && currentUserId === sale.buyerId) {
    component = (
      <div>
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
        />
      </div>
    );
  } else if (sale.status === 3 && currentUserId === sale.sellerId) {
    component = <NoActionAvailableTextBox />;
  } else if (sale.status === 4 && currentUserId === sale.sellerId) {
    component = (
      <div>
        <button
          className="btn btn-success btn-lg"
          data-toggle="modal"
          data-target="#confirmItemSentModal"
        >
          Confirm Item Sent To Buyer
        </button>
        <ConfirmItemSentContainer data={{ saleId: sale.id }} />
      </div>
    );
  } else if (sale.status === 4 && currentUserId === sale.buyerId) {
    component = <NoActionAvailableTextBox />;
  } else if (sale.status === 5 && currentUserId === sale.buyerId) {
    component = (
      <div>
        <button
          className="btn btn-success btn-lg"
          data-toggle="modal"
          data-target="#confirmItemRecievedModal"
        >
          Confirm Item Recieved
        </button>
        <ConfirmItemRecievedContainer data={{ saleId: sale.id }} />
      </div>
    );
  } else if (sale.status === 5 && currentUserId === sale.sellerId) {
    component = <NoActionAvailableTextBox />;
  }

  return props.data.isLoading ? <BorderSpinner /> : component;
}

export default SaleMenuComponent;
