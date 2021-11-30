import React, { Component } from "react";
import {
  Grid,
  Photo,
  GridControlBar,
  GridControlBarItem,
} from "react-instagram-ui-kit";
import { connect } from "react-redux";
import { compose } from "redux";
import { getImagesForItems } from "../../actions/actionsItems";
import { getConferencesForOwner } from "../../actions/actionsConference";

class OwnerConferences extends Component {
  state = {
    showPostModal: false,
    postId: 0,
    username: "",
  };
  async componentDidMount() {
    debugger;
    await this.props.getConferencesForOwner(
      sessionStorage.getItem("userIdWebShop")
    );
    await this.getAllImages(this.props.conferencesForOwner);
  }
  render() {
    if (this.props.conferencesForOwner === undefined) {
      return null;
    }

    debugger;
    const conferencesForOwner = this.props.conferencesForOwner;

    if (this.props.itemsImages === undefined) {
      return null;
    }
    debugger;
    const ItemsList = () => {
      debugger;
      return conferencesForOwner.map((post, i) =>
        this.props.itemsImages[i].contentType === "image/jpeg" ? (
          <Photo
            src={
              "data:image/jpg;base64," + this.props.itemsImages[i].fileContents
            }
            onClick={() => this.view(post)}
          />
        ) : (
          <video
            controls
            onClick={() => this.view(post)}
            style={{ width: 367, height: 370 }}
            className="mb-3"
          >
            <source
              src={
                "data:video/mp4;base64," +
                this.props.itemsImages[i].fileContents
              }
              type="video/mp4"
            ></source>
          </video>
        )
      );
    };

    return (
      <div>
        <Grid>
          <GridControlBar>
            <GridControlBarItem isActive>Yours Conferences</GridControlBarItem>
          </GridControlBar>
          <ItemsList />
        </Grid>
      </div>
    );
  }

  view(f) {
    localStorage.setItem("conference-id", f.id);
    window.location = "/conference/" + f.id;
  }

  getAllImages = async (items) => {
    debugger;
    const paths = [];
    for (var i = 0; i < items.length; i++) {
      if (items[i].imagePath === undefined) {
        paths.push(items[i].imagePaths[0]);
      } else {
        paths.push(items[i].imagePath);
      }
    }
    await this.props.getImagesForItems(paths);
  };
}

const mapStateToProps = (state) => ({
  conferencesForOwner: state.conferencesForOwner,
  itemsImages: state.itemsImages,
});

export default compose(
  connect(mapStateToProps, {
    getConferencesForOwner,
    getImagesForItems,
  })
)(OwnerConferences);
