﻿/**
* author:xishuai
* address:https://www.github.com/yuezhongxin/MessageManager
**/

using System;
using System.Collections.Generic;
using AutoMapper;
using MessageManager.Application.DTO;
using MessageManager.Domain;
using MessageManager.Domain.DomainModel;
using MessageManager.Domain.Repositories;

namespace MessageManager.Application.Implementation
{
    /// <summary>
    /// Message管理应用层接口实现
    /// </summary>
    public class MessageServiceImpl : ApplicationService, IMessageService
    {
        #region Private Fields
        private readonly IMessageRepository messageRepository;
        private readonly IUserRepository userRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// 初始化一个<c>MessageServiceImpl</c>类型的实例。
        /// </summary>
        /// <param name="context">用来初始化<c>MessageServiceImpl</c>类型的仓储上下文实例。</param>
        /// <param name="messageRepository">“消息”仓储实例。</param>
        /// <param name="userRepository">“用户”仓储实例。</param>
        public MessageServiceImpl(IRepositoryContext context,
            IMessageRepository messageRepository,
            IUserRepository userRepository)
            :base(context)
        {
            this.messageRepository = messageRepository;
            this.userRepository = userRepository;
        }
        #endregion

        #region IMessageService Members
        /// <summary>
        /// 通过发送方获取消息列表
        /// </summary>
        /// <param name="userDTO">发送方</param>
        /// <returns>消息列表</returns>
        public IEnumerable<MessageDTO> GetMessagesBySendUser(UserDTO sendUserDTO)
        {
            User user = userRepository.GetUserByName(sendUserDTO.Name);
            var messages = messageRepository.GetMessagesBySendUser(user);
            if (messages == null)
                return null;
            var ret = new List<MessageDTO>();
            foreach (var message in messages)
            {
                ret.Add(Mapper.Map<Message, MessageDTO>(message));
            }
            return ret;
        }
        /// <summary>
        /// 通过接受方获取消息列表
        /// </summary>
        /// <param name="userDTO">接受方</param>
        /// <returns>消息列表</returns>
        public IEnumerable<MessageDTO> GetMessagesByReceiveUser(UserDTO receiveUserDTO)
        {
            User user = userRepository.GetUserByName(receiveUserDTO.Name);
            var messages = messageRepository.GetMessagesByReceiveUser(user);
            if (messages == null)
                return null;
            var ret = new List<MessageDTO>();
            foreach (var message in messages)
            {
                ret.Add(Mapper.Map<Message, MessageDTO>(message));
            }
            return ret;
        }
        #endregion
    }
}