import React from "react";
import AddToCollectionPopupFormButton from "./AddToCollectionPopupFormButton";
import AddToCollectionPopupFormBody from "./AddToCollectionPopupFormBody";

const gradeOptions = [
  { id: 1, name: "Mint" },
  { id: 2, name: "Near Mint" },
  { id: 3, name: "Very Good" },
  { id: 4, name: "Good" },
  { id: 5, name: "Fair" },
  { id: 6, name: "Poor" }
];

function AddToCollectionPopupFormComponent(props) {
  return (
    <div>
      <AddToCollectionPopupFormButton
        data={{
          isReleaseAlreadyInCollection: props.data.isReleaseAlreadyInCollection
        }}
      />
      <AddToCollectionPopupFormBody
        data={{
          gradeOptions: gradeOptions,
          descriptionInput: props.data.descriptionInput,
          vinylGradeInput: props.data.vinylGradeInput,
          sleeveGradeInput: props.data.sleeveGradeInput
        }}
        functions={{
          handleOnChange: props.functions.handleOnChange,
          handleOnSubmit: props.functions.handleOnSubmit
        }}
      />
    </div>
  );
}

export default AddToCollectionPopupFormComponent;
