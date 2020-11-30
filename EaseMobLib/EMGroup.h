#pragma once
#include <emgroup.h>
#include "emsbsase.h"
#include "EMError.h"

using namespace System;
using namespace System::Collections::Generic;

namespace EaseMobLib {
	public enum EMGroupLeaveReason
	{
		BE_KICKED,				//User is kicked out by the group owner
		DESTROYED				//Group was destroyed by the group owner.
	};
	typedef cli::array<String^>^ EMGroupMemberList;
	public ref class EMGroup : EMBase
	{
	public:
		virtual ~EMGroup();

		/**
		* \brief Get group's ID.
		*
		* @param  NA
		* @return Group's ID.
		*/
		const String^ groupId();

		/**
		* \brief Get group's subject.
		*
		* @param  NA
		* @return Group's subject@
		*/
		const String^ groupSubject();

		/**
		* \brief Get group's description.
		*
		* @param  NA
		* @return Group's description
		*/
		const String^ groupDescription();

		/**
		* \brief Get group's owner.
		*
		* @param  NA
		* @return Group's owner
		*/
		const String^ groupOwner();

		/**
		* \brief Get current members count.
		*
		* Note: Will return 0 if have not ever got group's specification.
		* @param  NA
		* @return Members count
		*/
		int groupMembersCount();

		/**
		* \brief Get whether push is enabled status.
		*
		* @param  NA
		* @return Push status.
		*/
		bool isPushEnabled();

		/**
		* \brief Get whether group messages is blocked.
		*
		* Note: Group owner can't block group message.
		* @param  NA
		* @return Group message block status.
		*/
		bool isMessageBlocked();

	private:
		easemob::EMGroupPtr& getImpl();
	};

	typedef std::shared_ptr<EMGroup> EMGroupPtr;
	typedef cli::array<EMGroup^> EMGroupList;
}

