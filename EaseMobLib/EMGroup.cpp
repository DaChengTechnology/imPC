#include "pch.h"
#include "EMGroup.h"
#include "EMUtils.h"

namespace EaseMobLib {
	EMGroup::~EMGroup()
	{

	}

	/**
	* \brief Get group's ID.
	*
	* @param  NA
	* @return Group's ID.
	*/
	const String^ EMGroup::groupId() {
		return getCSString(getImpl()->groupId());
	}

	/**
	* \brief Get group's subject.
	*
	* @param  NA
	* @return Group's subject��
	*/
	const String^ EMGroup::groupSubject() {
		return getCSString(getImpl()->groupSubject());
	}

	/**
	* \brief Get group's description.
	*
	* @param  NA
	* @return Group's description
	*/
	const String^ EMGroup::groupDescription() {
		return getCSString(getImpl()->groupDescription());
	}

	/**
	* \brief Get group's owner.
	*
	* @param  NA
	* @return Group's owner
	*/
	const String^ EMGroup::groupOwner() {
		return getCSString(getImpl()->groupOwner());
	}

	/**
	* \brief Get current members count.
	*
	* Note: Will return 0 if have not ever got group's specification.
	* @param  NA
	* @return Members count
	*/
	int EMGroup::groupMembersCount() {
		return getImpl()->groupMembersCount();
	}

	/**
	* \brief Get whether push is enabled status.
	*
	* @param  NA
	* @return Push status.
	*/
	bool EMGroup::isPushEnabled() {
		return getImpl()->isPushEnabled();
	}

	/**
	* \brief Get whether group messages is blocked.
	*
	* Note: Group owner can't block group message.
	* @param  NA
	* @return Group message block status.
	*/
	bool EMGroup::isMessageBlocked() {
		return getImpl()->isMessageBlocked();
	}

	easemob::EMGroupPtr& EMGroup::getImpl() {
		return getNative<easemob::EMGroupPtr>();
	}
}