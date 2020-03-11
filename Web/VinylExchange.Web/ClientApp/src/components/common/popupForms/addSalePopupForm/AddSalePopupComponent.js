import React, { Component,Fragment } from "react";
import AddSalePopupFormBody from "./AddSalePopupFormBody";
import AddSalePopupFormButton from "./AddSalePopupFormButton";

const gradeOptions = [
  { id: 6, name: "Mint" },
  { id: 5, name: "Near Mint" },
  { id: 4, name: "Very Good" },
  { id: 3, name: "Good" },
  { id: 2, name: "Fair" },
  { id: 1, name: "Poor" }
];

function AddSalePopupComponent(props) {
  return (
    <Fragment>
      <AddSalePopupFormButton
       data={{collectionItemId:props.data.collectionItemId}}
       functions={{
          handleLoadColletionItemData:
            props.functions.handleLoadColletionItemData,
            handleLoadUserAddresses : props.functions.handleLoadUserAddresses
        }}
      />
      <AddSalePopupFormBody
        data={{
          collectionItemId:props.data.collectionItemId,
          gradeOptions: gradeOptions,
          descriptionInput: props.data.descriptionInput,
          vinylGradeInput: props.data.vinylGradeInput,
          sleeveGradeInput: props.data.sleeveGradeInput,
          priceInput: props.data.priceInput,
          shipsFromAddressSelectInput:props.data.shipsFromAddressSelectInput,
          userAddresses:props.data.userAddresses
        }}
        functions={{
          handleOnChange: props.functions.handleOnChange,
          handleOnSubmit: props.functions.handleOnSubmit,
          handleFlushModal: props.functions.handleFlushModal

        }}
      />
    </Fragment>
  );
}

export default AddSalePopupComponent;