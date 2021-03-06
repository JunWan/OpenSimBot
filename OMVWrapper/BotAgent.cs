﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using OpenSimBot.OMVWrapper.Command;

namespace OpenSimBot.OMVWrapper
{
    public class BotAgent
    {
        /*Members**************************************************************/
        private const string BOT_STATUS_MOVE = "MOVE";
        private const string BOT_STATUS_CHAT = "CHAT";

        private readonly Guid m_botGUID = Guid.NewGuid();
        private readonly BotInfo m_botInfo;
        private BotAssignment m_assignment = new BotAssignment();
        private Hashtable m_botStatus = new Hashtable();

        /*Attributes***********************************************************/
        public Guid ID
        {
            get { return m_botGUID; }
        }

        public BotInfo Info
        {
            get { return m_botInfo; }
        }

        public BotAssignment Assignment
        {
            get { return m_assignment; }
        }

        /*Functions************************************************************/
        public BotAgent(BotInfo botInfo)
        {
            m_botStatus[BOT_STATUS_MOVE] = null;
            m_botStatus[BOT_STATUS_CHAT] = null;

            m_botInfo = botInfo;
        }

        public void RegisterStatus(ICommand owner)
        {
            if (owner.Name == Command.Cmd_RandomMoving.CMD_NAME)
            {
                if (null != m_botStatus[BOT_STATUS_MOVE])
                {
                    ((ICommand)m_botStatus[BOT_STATUS_MOVE]).PostExecute();
                }
                m_botStatus[BOT_STATUS_MOVE] = owner;
            }

            if (owner.Name == Command.Cmd_RandomChating.CMD_NAME)
            {
                if (null != m_botStatus[BOT_STATUS_CHAT])
                {
                    ((ICommand)m_botStatus[BOT_STATUS_CHAT]).PostExecute();
                }
                m_botStatus[BOT_STATUS_CHAT] = owner;
            }
        }




        /*Class****************************************************************/
        public class BotInfo
        {
            /*Members**********************************************************/
            private readonly string m_firstname;
            private readonly string m_lastname;
            private readonly string m_password;
            private readonly string m_servURI;

            /*Attributes*******************************************************/
            public string Firstname
            {
                get { return m_firstname; }
            }

            public string Lastname
            {
                get { return m_lastname; }
            }

            public string Password
            {
                get { return m_password; }
            }

            public string Server
            {
                get { return m_servURI; }
            }
            
            /*Functions********************************************************/
            public BotInfo(string firstname,
                           string lastname,
                           string password,
                           string servURI)
            {
                m_firstname = firstname;
                m_lastname = lastname;
                m_password = password;
                m_servURI = servURI;
            }

            // The command will return the exceptions and errors while proceeding.
            private void HandleException()
            {

            }

            private void LogExcetption(string msg)
            {


            }

        }

        public class BotAssignment
        {
            /*Members**********************************************************/
            private List<TestStep> m_stepList = new List<TestStep>();
            private bool m_isFinished = false;

            /*Attributes*******************************************************/
            public bool IsFinished
            {
                get { return m_isFinished; }
                set { m_isFinished = value; }
            }

            /*Functions********************************************************/
            public void AddStep(TestStep step)
            {
                if (step != null)
                {
                    m_stepList.Add(step);
                }
            }

            public void ResetAssignment()
            {
                foreach (TestStep step in m_stepList)
                {
                    if (step.Status != TestStep.TestStatus.TESTSTEP_WAIT)
                    {
                        step.Status = TestStep.TestStatus.TESTSTEP_WAIT;
                    }
                }
            }

            public TestStep GetNextStep()
            {
                TestStep ret = null;
                foreach (TestStep step in m_stepList)
                {
                    if (step.Status == TestStep.TestStatus.TESTSTEP_WAIT)
                    {
                        ret = step;
                        break; 
                    }
                }

                return ret;
            }

            public TestStep GetStepByID(Guid id)
            {
                TestStep ret = null;
                foreach (TestStep step in m_stepList)
                {
                    if (Guid.Equals(id, step.ID))
                    {
                        ret = step;
                    }
                }

                return ret;
            }

            /*Class************************************************************/
            public class TestStep
            {
                /*Members******************************************************/
                public enum TestStatus
                {
                    TESTSTEP_WAIT = 0,
                    TESTSTEP_FAILE,
                    TESTSTEP_PROCESSING,
                    TESTSTEP_SUCESS,
                }
                private readonly Guid m_stepID = Guid.NewGuid();
                private readonly string m_name; 
                private readonly Hashtable m_paramList;
                private TestStatus m_status = TestStatus.TESTSTEP_WAIT;

                /*Attributes***************************************************/
                public string Name
                {
                    get { return m_name; }
                }

                public Hashtable Params
                {
                    get { return m_paramList; }
                }

                public TestStatus Status
                {
                    get { return m_status; }
                    set { m_status = value; }
                }

                public Guid ID
                {
                    get { return m_stepID; }
                }

                /*Functions****************************************************/
                public TestStep(string name, Hashtable paramList)
                {
                    m_name = name;
                    m_paramList = paramList;
                }
            }
        }
    }
}
