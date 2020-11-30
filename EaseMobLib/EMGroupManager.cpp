#include "pch.h"
#include "EMGroupManager.h"
#include "EMUtils.h"
#include "emgroupmanager_interface.h"
#include <vector>
#include "EMGroupInfo.h"
#include <vcclr.h>

using namespace System;
using namespace System::Threading;
using namespace System::Collections::Generic;

namespace EaseMobLib {
	public class EMGroupManagerListenerDelegate : easemob::EMGroupManagerListener {
	public:
		EMGroupManagerListenerDelegate() {
			lockObj = gcnew Object();
		}

		~EMGroupManagerListenerDelegate() {
			delete lockObj;
		}

		void addListener(EaseMobLib::EMGroupManagerListener^ listener) {
			Monitor::Enter(lockObj);
			bool contains = false;
			for (gcroot<EaseMobLib::EMGroupManagerListener^> _listener : listeners) {
				if (_listener->Equals(listener)) {
					contains = true;
					break;
				}
			}
			if (!contains) {
				listeners.push_back(listener);
			}
			Monitor::Exit(lockObj);
		}

		void removeListener(EaseMobLib::EMGroupManagerListener^ listener) {
			Monitor::Enter(lockObj);
			for (auto iter = listeners.begin(); iter != listeners.end(); iter++) {
				if ((*iter)->Equals(listener)) {
					listeners.erase(iter);
					break;
				}
			}
			Monitor::Exit(lockObj);
		}

		void clearListeners() {
			Monitor::Enter(lockObj);
			listeners.clear();
			Monitor::Exit(lockObj);
		}

		virtual void onReceiveInviteFromGroup(const std::string groupId, const std::string& inviter, const std::string& inviteMessage)
		{
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onReceiveInviteFromGroup != nullptr) {
					listener->onReceiveInviteFromGroup->Invoke(getCSString(groupId), getCSString(inviter), getCSString(inviteMessage));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onReceiveInviteAcceptionFromGroup(const easemob::EMGroupPtr group, const std::string& invitee)
		{
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onReceiveInviteAcceptionFromGroup != nullptr) {
					listener->onReceiveInviteAcceptionFromGroup->Invoke(getCSGroup(group), getCSString(invitee));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onReceiveInviteDeclineFromGroup(const easemob::EMGroupPtr group, const std::string& invitee, const std::string& reason) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onReceiveInviteDeclineFromGroup != nullptr) {
					listener->onReceiveInviteDeclineFromGroup->Invoke(getCSGroup(group), getCSString(invitee), getCSString(reason));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onAutoAcceptInvitationFromGroup(const easemob::EMGroupPtr group, const std::string& inviter, const std::string& inviteMessage) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onAutoAcceptInvitationFromGroup != nullptr) {
					listener->onAutoAcceptInvitationFromGroup->Invoke(getCSGroup(group), getCSString(inviter), getCSString(inviteMessage));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onLeaveGroup(const easemob::EMGroupPtr group, easemob::EMGroup::EMGroupLeaveReason reason) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onLeaveGroup != nullptr) {
					listener->onLeaveGroup->Invoke(getCSGroup(group), (EMGroupLeaveReason)reason);
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onReceiveJoinGroupApplication(const easemob::EMGroupPtr group, const std::string& from, const std::string& message) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onReceiveJoinGroupApplication != nullptr) {
					listener->onReceiveJoinGroupApplication->Invoke(getCSGroup(group), getCSString(from), getCSString(message));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onReceiveAcceptionFromGroup(const easemob::EMGroupPtr group) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onReceiveAcceptionFromGroup != nullptr) {
					listener->onReceiveAcceptionFromGroup->Invoke(getCSGroup(group));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onReceiveRejectionFromGroup(const std::string& groupId, const std::string& reason) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onReceiveRejectionFromGroup != nullptr) {
					listener->onReceiveRejectionFromGroup->Invoke(getCSString(groupId), getCSString(reason));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onUpdateMyGroupList(const std::vector<easemob::EMGroupPtr>& list) {
			array<EaseMobLib::EMGroup^>^ csList = gcnew array<EaseMobLib::EMGroup^>(list.size());
			int i = 0;
			for (const easemob::EMGroupPtr group : list) {
				EaseMobLib::EMGroup^ csGroup = getCSGroup(group);
				csList[i] = csGroup;
				i++;
			}

			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onUpdateMyGroupList != nullptr) {
					listener->onUpdateMyGroupList->Invoke(csList);
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onAddMutesFromGroup(const easemob::EMGroupPtr group, const std::vector<std::string>& mutes, int64_t muteExpire) {
			array<String^>^ csList = gcnew array<String^>(mutes.size());
			int i = 0;
			for (const std::string mute : mutes) {
				csList[i] = getCSString(mute);
			}

			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onAddMutesFromGroup != nullptr) {
					listener->onAddMutesFromGroup->Invoke(getCSGroup(group), csList, muteExpire);
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onRemoveMutesFromGroup(const easemob::EMGroupPtr group, const std::vector<std::string>& mutes) {
			array<String^>^ csList = gcnew array<String^>(mutes.size());
			int i = 0;
			for (const std::string mute : mutes) {
				csList[i] = getCSString(mute);
			}

			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onRemoveMutesFromGroup != nullptr) {
					listener->onRemoveMutesFromGroup->Invoke(getCSGroup(group), csList);
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onAddAdminFromGroup(const easemob::EMGroupPtr group, const std::string& admin) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onAddAdminFromGroup != nullptr) {
					listener->onAddAdminFromGroup->Invoke(getCSGroup(group), getCSString(admin));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onRemoveAdminFromGroup(const easemob::EMGroupPtr group, const std::string& admin) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onRemoveAdminFromGroup != nullptr) {
					listener->onRemoveAdminFromGroup->Invoke(getCSGroup(group), getCSString(admin));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void onAssignOwnerFromGroup(const easemob::EMGroupPtr group, const std::string& newOwner, const std::string& oldOwner) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onAssignOwnerFromGroup != nullptr) {
					listener->onAssignOwnerFromGroup(getCSGroup(group), getCSString(newOwner), getCSString(oldOwner));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void D_onMemberJoinedGroup(const easemob::EMGroupPtr group, const std::string& member) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onMemberJoinedGroup != nullptr) {
					listener->onMemberJoinedGroup(getCSGroup(group), getCSString(member));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void D_onMemberLeftGroup(const easemob::EMGroupPtr group, const std::string& member) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onMemberLeftGroup != nullptr) {
					listener->onMemberLeftGroup(getCSGroup(group), getCSString(member));
				}
			}
			Monitor::Exit(lockObj);
		}

		virtual void D_onUpdateAnnouncementFromGroup(const easemob::EMGroupPtr group, const std::string& announcement) {
			Monitor::Enter(lockObj);
			for each (EaseMobLib::EMGroupManagerListener ^ listener in listeners) {
				if (listener->onUpdateAnnouncementFromGroup != nullptr) {
					listener->onUpdateAnnouncementFromGroup(getCSGroup(group), getCSString(announcement));
				}
			}
			Monitor::Exit(lockObj);
		}

	private:
		gcroot<Object^> lockObj;
		std::vector<gcroot<EaseMobLib::EMGroupManagerListener^> > listeners;
	};

	EMGroupManager::EMGroupManager(easemob::EMGroupManagerInterface* p) {
		mImpl = p;
		listenerDelegate = new EMGroupManagerListenerDelegate();
		getImpl()->addListener((easemob::EMGroupManagerListener*)listenerDelegate);
	}

	EMGroupManager::~EMGroupManager() {
		getImpl()->removeListener((easemob::EMGroupManagerListener*)listenerDelegate);
		delete (easemob::EMGroupManagerListener*)listenerDelegate;
		listenerDelegate = nullptr;
	}

	/**
	* \brief Add a listener to group manager.
	*
	* @param  A group manager listener.
	* @return NA
	*/
	void EMGroupManager::addListener(EMGroupManagerListener^ listener) {
		((EMGroupManagerListenerDelegate*)this->listenerDelegate)->addListener(listener);
	}

	/**
	* \brief Remove a listener.
	*
	* @param  A group manager listener.
	* @return NA
	*/
	void EMGroupManager::removeListener(EMGroupManagerListener^ listener) {
		((EMGroupManagerListenerDelegate*)this->listenerDelegate)->removeListener(listener);
	}

	/**
	* \brief Remove all the listeners.
	*
	* @param  NA
	* @return NA
	*/
	void EMGroupManager::clearListeners() {
		((EMGroupManagerListenerDelegate*)this->listenerDelegate)->clearListeners();
	}

	/**
	* \brief Get a group with groupId, create the group if not exist.
	*
	* @param  Group's id.
	* @return A group with the groupId.
	*/
	EMGroup^ EMGroupManager::groupWithId(const String^ groupId) {
		easemob::EMGroupPtr group = getImpl()->groupWithId(extractCSString(groupId));
		easemob::EMGroupPtr ptr(group);
		EMGroup^ csGroup = gcnew EMGroup();
		csGroup->setNativeHandler<easemob::EMGroupPtr>(&ptr);
		return csGroup;
	}

	/**
	* \brief Get groups for login user from memory.
	*
	* @param  EMError used for output.
	* @return All user joined groups in memory.
	*/
	EMGroupList^ EMGroupManager::allMyGroups(EMError^ csError) {
		easemob::EMError error;
		easemob::EMGroupList groupList = getImpl()->allMyGroups(error);
		array<EMGroup^>^ csGroupList = gcnew array<EMGroup^>(groupList.size());
		int i = 0;
		for (easemob::EMGroupPtr& group : groupList) {
			easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
			EMGroup^ csGroup = getCSGroup(ptr);
			csGroupList[i] = csGroup;
			i++;
		}
		return csGroupList;
	}

	/**
	* \brief Get groups for login user from db.
	*
	* @return All user joined groups in db.
	*/
	EMGroupList^ EMGroupManager::loadAllMyGroupsFromDB() {
		easemob::EMError error;
		easemob::EMGroupList groupList = getImpl()->loadAllMyGroupsFromDB();
		array<EMGroup^>^ csGroupList = gcnew array<EMGroup^>(groupList.size());
		int i = 0;
		for (easemob::EMGroupPtr& group : groupList) {
			easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
			EMGroup^ csGroup = getCSGroup(ptr);
			csGroupList[i] = csGroup;
			i++;
		}
		return csGroupList;
	}

	/**
	* \brief Fetch all groups for login user from server.
	*
	* Note: Groups in memory will be updated.
	* @param  EMError used for output.
	* @return All user joined groups.
	*/
	EMGroupList^ EMGroupManager::fetchAllMyGroups(EMError^ csError) {
		easemob::EMError error;
		easemob::EMGroupList groupList = getImpl()->fetchAllMyGroups(error);
		easemob::EMErrorPtr ptr(new easemob::EMError(error));
		csError->setNativeHandler<easemob::EMErrorPtr>(&ptr);
		array<EMGroup^>^ csGroupList = gcnew array<EMGroup^>(groupList.size());
		int i = 0;
		for (easemob::EMGroupPtr& group : groupList) {
			easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
			EMGroup^ csGroup = getCSGroup(ptr);
			csGroupList[i] = csGroup;
			i++;
		}
		return csGroupList;
	}

	/**
	* \brief Fetch all groups for login user from server with page.
	*
	* Note: User can input 1 as first page number at the first time.
	* @param  Page's number.
	* @param  Page's size.
	* @param  EMError used for output.
	* @return current page's user joined groups.
	*/
	EMGroupList^ EMGroupManager::fetchAllMyGroupsWithPage(
		int pageNum,
		int pageSize,
		EMError^ csError)
	{
		easemob::EMError error;
		easemob::EMGroupList groupList = getImpl()->fetchAllMyGroups(error);
		easemob::EMErrorPtr ptr(new easemob::EMError(error));
		csError->setNativeHandler<easemob::EMErrorPtr>(&ptr);
		array<EMGroup^>^ csGroupList = gcnew array<EMGroup^>(groupList.size());
		int i = 0;
		for (easemob::EMGroupPtr& group : groupList) {
			easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
			EMGroup^ csGroup = getCSGroup(ptr);
			csGroupList[i] = csGroup;
			i++;
		}
		return csGroupList;
	}

	/**
	* \brief Fetch app's public groups.
	*
	* Note: User can input empty string as cursor at the first time.
	* @param  Page's cursor.
	* @param  Page's size.
	* @param  EMError used for output.
	* @return Wrapper of next page's cursor and current page result.
	*/
	EMCursorResult^ EMGroupManager::fetchPublicGroupsWithCursor(
		const String^ cursor,
		int pageSize,
		EMError^ csError)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMCursorResult cursorResult = getImpl()->fetchPublicGroupsWithCursor(extractCSString(cursor), pageSize, *(pErr.get()));

		List<EMGroupInfo^>^ csListGroupInfo = gcnew List<EMGroupInfo^>();
		for (std::vector<easemob::EMBaseObjectPtr>::const_iterator iter = cursorResult.result().begin();
			iter != cursorResult.result().end(); ++iter) {
			if ((easemob::EMGroup*)(*iter).get() == nullptr) {
				continue;
			}
			easemob::EMGroup* group = (easemob::EMGroup*)(*iter).get();
			EMGroupInfo^ groupInfo = gcnew EMGroupInfo(
				getCSString(group->groupId()), getCSString(group->groupSubject()));
			csListGroupInfo->Add(groupInfo);
		}
		array<EMBase^>^ csResult = gcnew array<EMBase^>(csListGroupInfo->Count);
		int i = 0;
		for each (EMGroupInfo ^ info in csListGroupInfo) {
			csResult[i] = (EMBase^)info;
			i++;
		}
		EMCursorResult^ csCursorResult = gcnew EMCursorResult(csResult, getCSString(cursorResult.nextPageCursor()));
		return csCursorResult;
	}

	/**
	* \brief Create a new group.
	*
	* Note: Login user will be the owner of created .
	* @param  Group's subject.
	* @param  Group's description.
	* @param  Welcome message that will be sent to invited user.
	* @param  Group's setting.
	* @param  Group's members.
	* @param  EMError used for output.
	* @return The created group.
	*/
	EMGroup^ EMGroupManager::createGroup(
		const String^ subject,
		const String^ description,
		const String^ welcomeMessage,
		EMGroupSetting^ csSetting,
		cli::array<String^>^ csMembers,
		EMError^ csError)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupSetting* setting = nullptr;
		if (csSetting != nullptr) {
			easemob::EMGroupSettingPtr& _setting = csSetting->getNative<easemob::EMGroupSettingPtr>();
			if (_setting.get() != nullptr) {
				setting = _setting.get();
			}
		}

		std::vector<std::string> vecMember;
		if (csMembers != nullptr) {
			for each (String ^ str in csMembers) {
				vecMember.push_back(extractCSString(str));
			}
		}

		easemob::EMGroupPtr group = getImpl()->createGroup(
			extractCSString(subject),
			extractCSString(description),
			extractCSString(welcomeMessage),
			*setting,
			vecMember,
			*(pErr.get()));

		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Join a public group.
	*
	* Note: The group's style must be PUBLIC_JOIN_OPEN, or will return error.
	* @param  Group's ID.
	* @param  EMError used for output.
	* @return The joined group.
	*/
	EMGroup^ EMGroupManager::joinPublicGroup(
		const String^ groupId,
		EMError^ csError
	)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->joinPublicGroup(extractCSString(groupId), *(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Apply to join a public group, need owner's approval.
	*
	* Note: The group's style must be PUBLIC_JOIN_APPROVAL, or will return error.
	* @param  Group's ID.
	* @param  Nick name in the group.
	* @param  Apply message, that will be sent to group owner.
	* @param  EMError used for output.
	* @return The group that try to join.
	*/
	EMGroup^ EMGroupManager::applyJoinPublicGroup(
		const String^ groupId,
		const String^ nickName,
		const String^ message,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->applyJoinPublicGroup(extractCSString(groupId), extractCSString(nickName), extractCSString(message), *(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Leave a group.
	*
	* Note: Group's owner can't leave the group.
	* @param  Group's ID.
	* @param  EMError used for output.
	* @return The leaved group.
	*/
	void EMGroupManager::leaveGroup(
		const String^ groupId,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		getImpl()->leaveGroup(extractCSString(groupId), *(pErr.get()));
	}

	/**
	* \brief Destroy a group.
	*
	* Note: Only group's owner can destroy the group.
	* @param  Group's ID.
	* @param  EMError used for output.
	* @return The destroyed group.
	*/
	void EMGroupManager::destroyGroup(
		const String^ groupId,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		getImpl()->destroyGroup(extractCSString(groupId), *(pErr.get()));
	}

	/**
	* \brief Add members to a group.
	*
	* Note: Maybe user don't have the priviledge, it depends on group's setting.
	* @param  Group's ID.
	* @param  Invited users.
	* @param  Welcome message that will be sent to invited user.
	* @param  EMError used for output.
	* @return The group that added members.
	*/
	EMGroup^ EMGroupManager::addGroupMembers(
		const String^ groupId,
		cli::array<String^>^ members,
		const String^ welcomeMessage,
		EMError^ csError
	) {
		std::vector<std::string> vecMembers;
		extractArray(members, vecMembers);

		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->addGroupMembers(extractCSString(groupId),
			vecMembers,
			extractCSString(welcomeMessage),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Remove members from a group.
	*
	* Note: Only group's owner can remove members.
	* @param  Group's ID.
	* @param  Removed users.
	* @param  EMError used for output.
	* @return The group that removed members.
	*/
	EMGroup^ EMGroupManager::removeGroupMembers(
		const String^ groupId,
		cli::array<String^>^ members,
		EMError^ csError
	) {
		std::vector<std::string> vecMembers;
		extractArray(members, vecMembers);

		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->removeGroupMembers(extractCSString(groupId),
			vecMembers,
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Block group's members, the blocked user can't send message in the group.
	*
	* Note: Only group's owner can block other members.
	* @param  Group's ID.
	* @param  Blocked users.
	* @param  EMError used for output.
	* @param  The reason that why block the members.
	* @return The group that blocked members.
	*/
	EMGroup^ EMGroupManager::blockGroupMembers(
		const String^ groupId,
		cli::array<String^>^ members,
		EMError^ csError,
		const String^ reason
	) {
		std::vector<std::string> vecMembers;
		extractArray(members, vecMembers);

		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->blockGroupMembers(extractCSString(groupId),
			vecMembers,
			*(pErr.get()),
			extractCSString(reason));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Unblock group's members.
	*
	* Note: Only group's owner can unblock other members.
	* @param  Group's ID.
	* @param  Unblocked users.
	* @param  EMError used for output.
	* @return The group that unblocked members.
	*/
	EMGroup^ EMGroupManager::unblockGroupMembers(
		const String^ groupId,
		cli::array<String^>^ members,
		EMError^ csError
	) {
		std::vector<std::string> vecMembers;
		extractArray(members, vecMembers);

		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->unblockGroupMembers(extractCSString(groupId),
			vecMembers,
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Change group's subject.
	*
	* Note: Only group's owner can change group's subject.
	* @param  Group's ID.
	* @param  The new group subject.
	* @param  EMError used for output.
	* @return The group that with new subject.
	*/
	EMGroup^ EMGroupManager::changeGroupSubject(
		const String^ groupId,
		const String^ newSubject,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->changeGroupSubject(
			extractCSString(groupId),
			extractCSString(newSubject),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Change group's description.
	*
	* Note: Only group's owner can change group's description.
	* @param  Group's ID.
	* @param  The new group description.
	* @param  EMError used for output.
	* @return The group that with new description.
	*/
	EMGroup^ EMGroupManager::changeGroupDescription(
		const String^ groupId,
		const String^ newDescription,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->changeGroupDescription(
			extractCSString(groupId),
			extractCSString(newDescription),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Get group's specification.
	*
	* @param  Group's ID.
	* @param  EMError used for output.
	* @param  Whether get group's members.
	* @return The group that update it's specification.
	*/
	EMGroup^ EMGroupManager::fetchGroupSpecification(
		const String^ groupId,
		EMError^ csError,
		bool fetchMembers
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->fetchGroupSpecification(
			extractCSString(groupId),
			*(pErr.get()),
			fetchMembers);
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}


	/**
	* \brief Search for a public group.
	*
	* @param  Group's ID.
	* @param  EMError used for output.
	* @return The search result.
	*/
	EMGroup^ EMGroupManager::searchPublicGroup(
		const String^ groupId,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->searchPublicGroup(
			extractCSString(groupId),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Block group message.
	*
	* Note: Owner can't block the group message.
	* @param  Group's ID.
	* @param  EMError used for output.
	* @return The group that blocked message.
	*/
	EMGroup^ EMGroupManager::blockGroupMessage(
		const String^ groupId,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->blockGroupMessage(
			extractCSString(groupId),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Unblock group message.
	*
	* @param  Group's ID.
	* @param  EMError used for output.
	* @return The group that unclocked message.
	*/
	EMGroup^ EMGroupManager::unblockGroupMessage(
		const String^ groupId,
		EMError^ csError
	) {
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->unblockGroupMessage(
			extractCSString(groupId),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief Accept user's join application.
	*
	* Note: Only group's owner can approval someone's application.
	* @param  Group's ID.
	* @param  The user that send the application.
	* @param  EMError used for output.
	* @return NA.
	*/
	void EMGroupManager::acceptJoinGroupApplication(
		const String^ groupId,
		const String^ user,
		EMError^ csError)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		getImpl()->acceptJoinGroupApplication(
			extractCSString(groupId),
			extractCSString(user),
			*(pErr.get()));
	}

	/**
	* \brief Reject user's join application.
	*
	* Note: Only group's owner can reject someone's application.
	* @param  Group's ID.
	* @param  The user that send the application.
	* @param  The reject reason.
	* @param  EMError used for output.
	* @return NA.
	*/
	void EMGroupManager::declineJoinGroupApplication(
		const String^ groupId,
		const String^ user,
		const String^ reason,
		EMError^ csError)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		getImpl()->declineJoinGroupApplication(
			extractCSString(groupId),
			extractCSString(user),
			extractCSString(reason),
			*(pErr.get()));
	}

	/**
	* \brief accept group's invitation.
	*
	* @param  Group's ID.
	* @param  Inviter.
	* @param  EMError used for output.
	* @return The group user has accepted.
	*/
	EMGroup^ EMGroupManager::acceptInvitationFromGroup(
		const String^ groupId,
		const String^ inviter,
		EMError^ csError)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		easemob::EMGroupPtr group = getImpl()->acceptInvitationFromGroup(
			extractCSString(groupId),
			extractCSString(inviter),
			*(pErr.get()));
		easemob::EMGroupPtr* ptr = new easemob::EMGroupPtr(group);
		return getCSGroup(ptr);
	}

	/**
	* \brief decline group's invitation.
	*
	* @param  Group's ID.
	* @param  Inviter.
	* @param  The decline reason.
	* @param  EMError used for output.
	* @return NA.
	*/
	void EMGroupManager::declineInvitationFromGroup(
		const String^ groupId,
		const String^ inviter,
		const String^ reason,
		EMError^ csError)
	{
		easemob::EMErrorPtr& pErr = csError->getNative<easemob::EMErrorPtr>();
		getImpl()->declineInvitationFromGroup(
			extractCSString(groupId),
			extractCSString(inviter),
			extractCSString(reason),
			*(pErr.get()));
	}

	easemob::EMGroupManagerInterface* EMGroupManager::getImpl() {
		return mImpl;
	}
}