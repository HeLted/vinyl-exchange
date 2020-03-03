import React from "react";
import PlayerLoaderButton from "./../../../common/PlayerLoaderButton";
import AddtoColletionPopupFormContainer from "./../../../common/popupForms/addToCollectionPopupForm/AddToCollectionPopupFormContainer"


function ReleaseMenuComponent(props) {
  return (
    <div className="btn-group" role="group">
      <PlayerLoaderButton data={{ releaseId: props.data.releaseId }} />
      <AddtoColletionPopupFormContainer data={{ releaseId: props.data.releaseId }}/>
    </div>
  );
}

export default ReleaseMenuComponent;
