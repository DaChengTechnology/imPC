#include "pch.h"
#include "EMVideoMessageBody.h"
#include "EMUtils.h"

namespace EaseMobLib {
	/**
	* \brief Video message body constructor.
	*
	* @param  NA
	* @return NA
	*/
	EMVideoMessageBody::EMVideoMessageBody() : EMVideoMessageBody("", "") {
	}

	/**
	* \brief Video message body constructor.
	*
	* @param  Video attachment local path.
	* @param  Video thumbnail local path.
	* @return NA
	*/
	EMVideoMessageBody::EMVideoMessageBody(const String^ localPath, const String^ thumbnailLocalPath)
		: EMFileMessageBody(EMMessageBodyType::VIDEO) {
		easemob::EMVideoMessageBodyPtr body(new easemob::EMVideoMessageBody(extractCSString(localPath), extractCSString(thumbnailLocalPath)));
		setNativeHandler<easemob::EMTextMessageBodyPtr>(&body);
	}

	/**
	* \brief Copy constructor.
	*
	* Note: Only copy the file path.
	* @param  Another video message body.
	* @return NA
	*/
	EMVideoMessageBody::EMVideoMessageBody(const EMVideoMessageBody^ body)
		: EMFileMessageBody(EMMessageBodyType::VIDEO) {
		nativeInit<easemob::EMVideoMessageBodyPtr>(body);
	}

	/**
	* \brief Assign operator overload.
	*
	* Note: Only copy the file path.
	* @param  Another video message body.
	* @return The image message body.
	*/
	EMVideoMessageBody% EMVideoMessageBody::operator = (const EMVideoMessageBody% body) {
		nativeInit<easemob::EMVideoMessageBodyPtr>(% body);
		return *this;
	}

	/**
	* \brief Class destructor.
	*
	* @param  NA
	* @return NA
	*/
	EMVideoMessageBody::~EMVideoMessageBody() {
		nativeFinalize<easemob::EMVideoMessageBodyPtr>();
	}

	/**
	* \brief Set local path of the thumbnail.
	*
	* @param  The local path.
	* @return NA
	*/
	void EMVideoMessageBody::setThumbnailLocalPath(const String^ localPath) {
		getImpl()->setThumbnailLocalPath(extractCSString(localPath));
	}

	/**
	* \brief Get local path of the thumbnail.
	*
	* @param  NA
	* @return The local path.
	*/
	const String^ EMVideoMessageBody::thumbnailLocalPath() {
		return getCSString(getImpl()->thumbnailLocalPath());
	}

	/**
	* \brief Set remote path of the thumbnail.
	*
	* Note: It's internal used, user should never need to call this method.
	* @param  The remote path.
	* @return NA
	*/
	void EMVideoMessageBody::setThumbnailRemotePath(const String^ remotePath) {
		getImpl()->setThumbnailRemotePath(extractCSString(remotePath));
	}

	/**
	* \brief Get remote path of the thumbnail.
	*
	* @param  NA
	* @return The remote path.
	*/
	const String^ EMVideoMessageBody::thumbnailRemotePath() {
		return getCSString(getImpl()->thumbnailRemotePath());
	}

	/**
	* \brief Set secret key of the thumbnail.
	*
	* Note: It's internal used, user should never need to call this method.
	* @param  The secret key.
	* @return NA
	*/
	void EMVideoMessageBody::setThumbnailSecretKey(const String^ secretKey) {
		getImpl()->setThumbnailSecretKey(extractCSString(secretKey));
	}

	/**
	* \brief Get secret key of the thumbnail.
	*
	* @param  NA
	* @return The secret key.
	*/
	const String^ EMVideoMessageBody::thumbnailSecretKey() {
		return getCSString(getImpl()->thumbnailSecretKey());
	}

	/**
	* \brief Set download status of the thumbnail.
	*
	* Note: Usually, user should NOT call this method directly.
	* @param  The download status.
	* @return NA
	*/
	void EMVideoMessageBody::setThumbnailDownloadStatus(EMDownloadStatus status) {
		getImpl()->setThumbnailDownloadStatus((easemob::EMFileMessageBody::EMDownloadStatus)status);
	}

	/**
	* \brief Get download status of the thumbnail.
	*
	* @param  NA
	* @return The download status.
	*/
	EMDownloadStatus EMVideoMessageBody::thumbnailDownloadStatus() {
		return (EMDownloadStatus)getImpl()->thumbnailDownloadStatus();
	}

	/**
	* \brief Set size of the video.
	*
	* @param  The video's size.
	* @return NA
	*/
	void EMVideoMessageBody::setSize(const Size^ size) {
		getImpl()->setSize(easemob::EMVideoMessageBody::Size(size->mWidth, size->mHeight));
	}

	/**
	* \brief Get size of the video.
	*
	* @param  NA
	* @return The video size.
	*/
	const EMVideoMessageBody::Size^ EMVideoMessageBody::size() {
		easemob::EMVideoMessageBody::Size size = getImpl()->size();
		return gcnew Size(size.mWidth, size.mHeight);
	}

	/**
	* \brief Get playing duration of the video.
	*
	* @param  NA
	* @return The video playing duration.
	*/
	int EMVideoMessageBody::duration() {
		return getImpl()->duration();
	}

	/**
	* \brief Set playing duration of the video.
	*
	* @param  The video's playing duration.
	* @return NA
	*/
	void EMVideoMessageBody::setDuration(int duration) {
		getImpl()->setDuration(duration);
	}

	easemob::EMVideoMessageBodyPtr& EMVideoMessageBody::getImpl() {
		return getNative<easemob::EMVideoMessageBodyPtr>();
	}
}