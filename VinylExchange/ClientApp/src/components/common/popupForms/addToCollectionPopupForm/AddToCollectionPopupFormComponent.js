import React from "react";
import AddToCollectionPopupFormButton from "./AddToCollectionPopupFormButton";
import AddToCollectionPopupFormBody from "./AddToCollectionPopupFormBody";

const gradeOptions = [
  { id: 6, name: "Mint" },
  { id: 5, name: "Near Mint" },
  { id: 4, name: "Very Good" },
  { id: 3, name: "Good" },
  { id: 2, name: "Fair" },
  { id: 1, name: "Poor" }
];

function AddToCollectionPopupFormComponent(props) {
  return (
    <div>
      <AddToCollectionPopupFormButton
        data={{
          isReleaseAlreadyInUserCollection: props.data.isReleaseAlreadyInUserCollection
        }}
      />
      <AddToCollectionPopupFormBody
        data={{
          gradeOptions: gradeOptions,
          descriptionInput: props.data.descriptionInput,
          vinylGradeInput: props.data.vinylGradeInput,
          sleeveGradeInput: props.data.sleeveGradeInput,
       
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
