import React from "react";
import BorderSpinner from "./../../../common/spinners/BorderSpinner";
import PlaceOrderModalContainer from "./saleMenuModals/placeOrder/PlaceOrderModalContainer";
import SetShippingPriceModalContainer from "./saleMenuModals/setShippingPrice/SetShippingPriceModalContainer";
import PayNowModalContainer from "./saleMenuModals/payNow/PayNowModalContainer";
import NoActionAvailableTextBox from "./../../../common/divTextBoxes/NoActionAvailableTextBox";
import ConfirmItemSentModalContainer from "./saleMenuModals/confirmItemSent/ConfirmItemSentModalContainer";
import ConfirmItemRecievedModalContainer from "./saleMenuModals/confirmItemRecieved/ConfirmItemRecievedModalContainer";
import EditSaleModalContainer from "./saleMenuModals/editSale/EditSaleModalContainer";
import RemoveSaleModalContainer from "./saleMenuModals/removeSale/RemoveSaleModalContainer";


function SaleMenuComponent(props) {
  let component = null;
  const sale = props.data.sale;
  const currentUserId = props.data.currentUserId;

  if (sale.status === 1 && currentUserId === sale.sellerId) {
    component = (
      <div>
      <button
          className="btn btn-danger btn-lg"
          data-toggle="modal"
          data-target="#removeSaleModal"
        >
          Remove Sale
        </button>
        <RemoveSaleModalContainer data={{saleId:sale.id}}/>
        <button
          className="btn btn-primary btn-lg"
          data-toggle="modal"
          data-target="#editSaleModal"
        >
          Edit Sale
        </button>
        <EditSaleModalContainer data={{ sale: sale }} />
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
        <ConfirmItemSentModalContainer data={{ saleId: sale.id }} />
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
        <ConfirmItemRecievedModalContainer data={{ saleId: sale.id }} />
      </div>
    );
  } else if (sale.status === 5 && currentUserId === sale.sellerId) {
    component = <NoActionAvailableTextBox />;
  } else if (sale.status === 6) {
    component = <NoActionAvailableTextBox />;
  }

  return props.data.isLoading ? <BorderSpinner /> : component;
}

export default SaleMenuComponent;
