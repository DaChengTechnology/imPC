#pragma once
#include <Windows.h>
#include <WavePlayer.h>
using namespace System;
using namespace System::Collections::Generic;
using namespace System::Runtime::InteropServices;

namespace AMRPlayer {
	public ref class AMRPlayer
	{
		// TODO: 在此处为此类添加方法。
	public:
        AMRPlayer();
        ~AMRPlayer();
        // 播放
        void Play(String^ lpszFile, [Out] UInt32% pLength);
        // 暂停
        void Pause();
        // 继续
        void Resume();
        // 停止
        void Stop();
        // 播放时间
        UInt32 GetAudioLength(String^ lpszFile);
        // 音量
        void SetVolume(UInt32 dwVolume); // volume: 0-1000
        bool IsPlaying();
    private:
        WavePlayer* player;
	};
}
