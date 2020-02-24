import React, { Component,Fragment } from "react";
import AddSalePopupFormBody from "./AddSalePopupFormBody";
import AddSalePopupFormButton from "./AddSalePopupFormButton";

const gradeOptions = [
  { id: 1, name: "Mint" },
  { id: 2, name: "Near Mint" },
  { id: 3, name: "Very Good" },
  { id: 4, name: "Good" },
  { id: 5, name: "Fair" },
  { id: 6, name: "Poor" }
];

function AddSalePopupComponent(props) {
  return (
    <Fragment>
      <AddSalePopupFormButton
       data={{collectionItemId:props.data.collectionItemId}}
       functions={{
          handleLoadColletionItemData:
            props.functions.handleLoadColletionItemData
        }}
      />
      <AddSalePopupFormBody
        data={{
          collectionItemId:props.data.collectionItemId,
          gradeOptions: gradeOptions,
          descriptionInput: props.data.descriptionInput,
          vinylGradeInput: props.data.vinylGradeInput,
          sleeveGradeInput: props.data.sleeveGradeInput,
          priceInput: props.data.priceInput
        }}
        functions={{
          handleOnChange: props.functions.handleOnChange,
          handleOnSubmit: props.functions.handleOnSubmit
        }}
      />
    </Fragment>
  );
}

export default AddSalePopupComponent;
