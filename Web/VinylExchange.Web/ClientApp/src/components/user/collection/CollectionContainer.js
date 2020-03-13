import React, { Component } from "react";
import CollectionComponent from "./CollectionComponent";
import axios from "axios";
import { Url, Controllers } from "./../../../constants/UrlConstants";
import PulseLoader from "react-spinners/PulseLoader";
import { NotificationContext } from "./../../../contexts/NotificationContext";
import getAntiForgeryAxiosConfig from "./../../../functions/getAntiForgeryAxiosConfig";

class CollectionContainer extends Component {
  constructor() {
    super();
    this.state = {
      collectionItems: [],
      isLoading: false
    };
  }

  static contextType = NotificationContext

  componentDidMount() {
    this.getUserCollection();
  }

  getUserCollection = () =>{
    this.setState({isLoading:true})
   
      axios
        .get(
          Url.api +
            Controllers.collections.name +
            Controllers.collections.actions.getUserCollection +
            Url.slash
        )
        .then(response => {
          this.setState({ collectionItems: response.data,isLoading:false });
          this.context.handleAppNotification("Loaded user collection",5)
        })
        .catch(error => {
          this.context.handleServerNotification(error.response)
        });
  

  }

  handleRemoveFromCollection = collectionItemId => {
    axios.delete(
      Url.api + Controllers.collections.name + Url.slash + collectionItemId,
      getAntiForgeryAxiosConfig()

    ).then(response => {
      this.context.handleAppNotification("Sucessfully removed release from collection",4)
      this.getUserCollection();
    })
    .catch(error => {
      this.context.handleServerNotification(error.response,"Error while trying to delete item from collection!");
    });
  };

  render() {
    const collectionComponents = this.state.collectionItems.map(
      collectionItem => {
        return (
            <CollectionComponent
              data={{
                id: collectionItem.id,
                vinylGrade: collectionItem.vinylGrade,
                sleeveGrade: collectionItem.sleeveGrade,
                description: collectionItem.description,
                releaseId: collectionItem.releaseId,
                artist: collectionItem.artist,
                title: collectionItem.title,
                coverArt:
                  Url.mediaStorage +
                  collectionItem.coverArt.path +
                  collectionItem.coverArt.fileName
              }}
              functions={{
                handleRemoveFromCollection: this.handleRemoveFromCollection
              }}
              key={collectionItem.id}
            />
         
        );
      }
    );


    const loader = (<PulseLoader
    size={15}
    //size={"150px"} this also works
    color={"lime"}
    loading={this.state.isLoading}
  />)

    return (
      <div className="collection-container container-fluid">
        <div className="row border text-center">
          <div className="col-12">
            <h2 className="property-text">User Collection</h2>
            </div>
          </div>
        <div className="row border justify" style={{padding:"100px",height:"900px",overflowY:"scroll"}}>{this.state.isLoading ? loader : collectionComponents}</div>
      </div>
    );
  }
}

export default CollectionContainer;
